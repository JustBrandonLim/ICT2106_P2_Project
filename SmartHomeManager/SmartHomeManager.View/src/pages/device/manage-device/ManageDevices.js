import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack, Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, useDisclosure, FormControl, FormLabel, Input, FormHelperText, useToast } from "@chakra-ui/react";
import { createSearchParams, useNavigate } from "react-router-dom";
import ManageDeviceSelectionCard from "components/Devices/ManageDeviceSelectionCard";
import axios from "axios";

export default function ManageDevices() {
  const tempId = "11111111-1111-1111-1111-111111111111";  
  const [devices, setDevices] = useState([]);
  const [assignedDevices, setAssignedDevices] = useState([]);
  const [unassignedDevices, setUnassignedDevices] = useState([]);

  const [selectedDeviceToManagePassword, setSelectedDeviceToManagePassword] = useState(null);
  const [destination, setDestination] = useState(0);

  const [showSetPasswordModal, setShowSetPasswordModal] = useState(false);
  const [showCheckPasswordModal, setShowCheckPasswordModal] = useState(false);

  const [accountId, setAccountId] = useState("");
  const [password1, setPassword1] = useState("");
  const [password2, setPassword2] = useState("");

  // for modal usage
  const { isOpen, onOpen, onClose } = useDisclosure();
  const toast = useToast();
  const navigate = useNavigate();

  function handleCloseModal() {
    onClose();
    resetPasswordHooks();
  }

  function handleSetPassword(e, i) {
    e.preventDefault();
    setShowSetPasswordModal(true);
    // on click button
    if (i != null) {
      onOpen();
      setSelectedDeviceToManagePassword(i);
    }
    // on form submit
    else {
      if (password1 && (password1 == password2)) {
        axios.post('https://localhost:7140/api/ManageDevice/SetDevicePasswordById', {
          deviceId: devices[selectedDeviceToManagePassword].deviceId,
          devicePassword: password1
        })
          .then(function (response) {
            console.log(response);
            if (response.status == 200) {
              handleCloseModal();
              toast({
                title: "Success",
                description: "Password has been set successfully.",
                status: "success",
                duration: 9000,
                isClosable: true,
              })
              fetchData();
            }
          })
          .catch(function (error) {
            console.log(error);
            toast({
              title: "Error",
              description: "Set password failed.",
              status: "error",
              duration: 9000,
              isClosable: true,
            });
          })
      }
      else {
        toast({
          title: "Error",
          description: "Password does not match! Please try again...",
          status: "error",
          duration: 9000,
          isClosable: true,
        });
      }
    }
  }

  function handleCheckPassword(e, i) {
    e.preventDefault();
    setShowCheckPasswordModal(true);
    // on click button
    if (i != null) {
      onOpen();
      setSelectedDeviceToManagePassword(i);
    }
    // on form submit
    else {
      if (password1) {
        let device = devices[selectedDeviceToManagePassword];
        if (password1 == device.devicePassword) { 
          handleCloseModal();
          if (destination == 1) navigateToSettings(device)
          else navigateToConfiguration(device)
        }
        else {
          toast({
            title: "Error",
            description: "Password lock failed.",
            status: "error",
            duration: 9000,
            isClosable: true,
          })
        }
      }
  }
}

// reset password hooks including remove selected device
function resetPasswordHooks() {
  setShowSetPasswordModal(false);
  setShowCheckPasswordModal(false);
  setSelectedDeviceToManagePassword();
  setPassword1("");
  setPassword2("");
}

function handleManageSettings(e, device, i) {
  e.preventDefault();

  // if requires password, get input
  if (device.devicePassword) {
    handleCheckPassword(e, i);
    setDestination(1);
  }
  else navigateToSettings(device);
}

function handleManageConfiguration(e, device, i) {
  e.preventDefault();

  // if requires password, get input
  if (device.devicePassword) {
    handleCheckPassword(e, i);
    setDestination(2);
  }
  else navigateToConfiguration(device);
}

function navigateToSettings(device) {
  navigate({
    pathname: "/managedevicesettings",
    search: `?${createSearchParams({
      deviceId: device.deviceId,
      deviceName: device.deviceName,
      deviceBrand: device.deviceBrand,
      deviceModel: device.deviceModel,
    })}`,
  });
}

function navigateToConfiguration(device) {
  navigate({
    pathname: "/managedeviceconfiguration",
    search: `?${createSearchParams({
      deviceId: device.deviceId,
      deviceName: device.deviceName,
      deviceBrand: device.deviceBrand,
      deviceModel: device.deviceModel,
    })}`,
  });
}

function fetchData() {
  fetch(`https://localhost:7140/api/ManageDevice/GetAllDevicesByAccount/${accountId}`)
  .then((response) => response.json())
  .then((data) => {
    setDevices(data);
    setAssignedDevices(
      data.filter((device) => {
        return device.roomId;
      })
    );

    setUnassignedDevices(
      data.filter((device) => {
        return !device.roomId;
      })
    );
  });
}

useEffect(() => {
    const id = JSON.parse(localStorage.getItem('accountId'));
    if (id) setAccountId(id)
    else setAccountId(tempId);
    fetchData();
  }, [])
  
useEffect(() => {
    if (accountId) fetchData();
}, [accountId])

return (
  <Container mt={5} mb={5} p={5} maxW="2xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
    <Text fontWeight="bold" fontSize="xl" textTransform="uppercase" mb={5}>
      Manage Devices
    </Text>

    {showSetPasswordModal &&
      <Modal isOpen={isOpen} onClose={handleCloseModal}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Set Device Password</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <form
              id="set-password-form"
              onSubmit={(e) => handleSetPassword(e)}
            >
              <FormControl isRequired>
                <FormLabel>Password</FormLabel>
                <Input type="password" onChange={(e) => setPassword1(e.target.value)} />
                <FormLabel>Confirm Password</FormLabel>
                <Input type="password" onChange={(e) => setPassword2(e.target.value)} />
                <FormHelperText>
                  We keep your device secure
                </FormHelperText>
              </FormControl>
            </form>
          </ModalBody>

          <ModalFooter>
            <Button type="submit" form="set-password-form" colorScheme='green' mr={3}>
              Set
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    }

    {showCheckPasswordModal &&
      <Modal isOpen={isOpen} onClose={handleCloseModal}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Please Enter Device Password</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <form
              id="set-password-form"
              onSubmit={(e) => handleCheckPassword(e)}
            >
              <FormControl isRequired>
                <FormLabel>Password</FormLabel>
                <Input type="password" onChange={(e) => setPassword1(e.target.value)} />
              </FormControl>
            </form>
          </ModalBody>

          <ModalFooter>
            <Button type="submit" form="set-password-form" colorScheme='green' mr={3}>
              Submit
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    }

    <Tabs w="full">
      <TabList>
        <Tab>All</Tab>
        <Tab>Assigned</Tab>
        <Tab>Unassigned</Tab>
      </TabList>
      <TabPanels>
        <TabPanel>
          <Stack spacing={5}>
            {devices.length > 0 ? (
              devices.map((device, i) => (
                <ManageDeviceSelectionCard
                  handleSetPassword={!device.devicePassword ? (event) => handleSetPassword(event, i) : null}
                  handleManageSettings={(event) => handleManageSettings(event, device, i)}
                  handleManageConfiguration={(event) => handleManageConfiguration(event, device, i)}
                  key={i}
                  deviceId={device.deviceId}
                  deviceSerialNumber={device.deviceSerialNumber}
                  deviceName={device.deviceName}
                  deviceModel={device.deviceModel}
                  deviceBrand={device.deviceBrand}
                />
              ))
            ) : (
              <p>None available.</p>
            )}
          </Stack>
        </TabPanel>
        <TabPanel>
          <Stack spacing={5}>
            {assignedDevices.length > 0 ? (
              assignedDevices.map((device, i) => (
                <ManageDeviceSelectionCard
                  handleManageSettings={(event) => handleManageConfiguration(event, device, i)}
                  handleManageConfiguration={(event) => handleManageConfiguration(event, device, i)}
                  key={i}
                  deviceId={device.deviceId}
                  deviceSerialNumber={device.deviceSerialNumber}
                  deviceName={device.deviceName}
                  deviceModel={device.deviceModel}
                  deviceBrand={device.deviceBrand}
                />
              ))
            ) : (
              <p>None available.</p>
            )}
          </Stack>
        </TabPanel>
        <TabPanel>
          <Stack spacing={5}>
            {unassignedDevices.length > 0 ? (
              unassignedDevices.map((device, i) => (
                <ManageDeviceSelectionCard
                  handleManageSettings={(event) => handleManageConfiguration(event, device, i)}
                  handleManageConfiguration={(event) => handleManageConfiguration(event, device, i)}
                  key={i}
                  deviceId={device.deviceId}
                  deviceSerialNumber={device.deviceSerialNumber}
                  deviceName={device.deviceName}
                  deviceModel={device.deviceModel}
                  deviceBrand={device.deviceBrand}
                />
              ))
            ) : (
              <p>None available.</p>
            )}
          </Stack>
        </TabPanel>
      </TabPanels>
    </Tabs>
  </Container>
);
}