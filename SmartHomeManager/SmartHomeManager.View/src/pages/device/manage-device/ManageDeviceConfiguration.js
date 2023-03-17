import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Button, Stack, useToast, useDisclosure, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, FormControl, FormLabel, Input, FormHelperText } from "@chakra-ui/react";
import { useSearchParams } from "react-router-dom";
import ManageDeviceConfigurationCard from "../../../components/Devices/ManageDeviceConfigurationCard";

export default function ManageDeviceConfiguration() {
  const [searchParams] = useSearchParams();
  const deviceId = searchParams.get("deviceId");
  const deviceName = searchParams.get("deviceName");
  const deviceBrand = searchParams.get("deviceBrand");
  const deviceModel = searchParams.get("deviceModel");
  const [devicePossibleConfigurations, setDevicePossibleConfigurations] = useState([]);
  const [deviceActualConfigurations, setDeviceActualConfigurations] = useState([]);

      // for export device
  const [exportDevice, setExportDevice] = useState("");

      // for import device
  const [showImportDeviceModal, setShowImportDeviceModal] = useState(false);


  const toast = useToast();

  const { isOpen, onOpen, onClose } = useDisclosure();

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

  function handleCloseModal() {
    onClose();
    resetImportHooks();
  }

  function resetImportHooks() {
    setShowImportDeviceModal(false);
    
    }

  useEffect(() => {
      fetchDeviceConfigurations()
  }, []);

    function fetchDeviceConfigurations() {
        fetch(`https://localhost:7140/api/ManageDevice/GetDevicePossibleConfigurations/${deviceBrand}/${deviceModel}`)
            .then((response) => response.json())
            .then((data) => {
                setDevicePossibleConfigurations(data);
            });
        fetch(`https://localhost:7140/api/ManageDevice/GetDeviceConfigurations/${deviceId}/${deviceBrand}/${deviceModel}`)
            .then((response) => response.json())
            .then((data) => {
                setDeviceActualConfigurations(data);
            });
    }


    function handleDeviceConfiguration({ configurationKey, configurationValue }) {
        fetch("https://localhost:7140/api/ManageDevice/ApplyDeviceConfiguration", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                configurationKey,
                deviceBrand,
                deviceModel,
                deviceId,
                configurationValue,
            }),
        })
            .then((response) => {
                if (response.ok) {
                    toast({
                        title: "Success",
                        description: "Device Configuration has been added successfully.",
                        status: "success",
                        duration: 9000,
                        isClosable: true,
                    });
                } else {
                    toast({
                        title: "Error",
                        description: "Device Configuration adding failed.",
                        status: "error",
                        duration: 9000,
                        isClosable: true,
                    });
                }
                fetchDeviceConfigurations();
                onClose();
            });
    }

    function handleExportDevice(e) {
      e.preventDefault();
      fetch(`https://localhost:7140/api/ManageDevice/ExportDeviceConfigurations/${deviceId}/${deviceBrand}/${deviceModel}`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            deviceId: deviceId,
            deviceBrand: deviceBrand,
            deviceModel: deviceModel,
          }),
      })
          .then((response) => {
              if (response.ok) {
                  toast({
                      title: "Success",
                      description: "Device has been exported successfully.",
                      status: "success",
                      duration: 9000,
                      isClosable: true,
                  });
                  return response.json(); // parse the response and return data
              } else {
                  toast({
                      title: "Error",
                      description: "Device export failed.",
                      status: "error",
                      duration: 9000,
                      isClosable: true,
                  });
              }
          })
          .then((data) => {
            setExportDevice(data);
          })
          .catch((error) => {
              console.error('Error exporting device:', error);
          });
    }

function setImportDevice(e) {
  e.preventDefault();
  setShowImportDeviceModal(true);
  onOpen();
}

    
function handleImportDevice(e, deviceConfigurationJson ) {
  e.preventDefault();
  fetch(`https://localhost:7140/api/ManageDevice/ImportDeviceConfigurations/${deviceId}/${deviceConfigurationJson}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        deviceId: deviceId,
        deviceBrand: deviceBrand,
        deviceModel: deviceModel,
      }),
  })
      .then((response) => {
          if (response.ok) {
              toast({
                  title: "Success",
                  description: "Device has been imported successfully.",
                  status: "success",
                  duration: 9000,
                  isClosable: true,
              });
              return response.json(); // parse the response and return data
          } else {
              toast({
                  title: "Error",
                  description: "Device import failed.",
                  status: "error",
                  duration: 9000,
                  isClosable: true,
              });
          }
      })
}


  if (devicePossibleConfigurations.length <= 0 || deviceActualConfigurations.length <= 0) {
    return <Text> Loading </Text>;
  } else
    return (
      <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
        <Text fontWeight="bold" fontSize="xl" mb={5}>
          {`Manage ${deviceName} configurations`}
        </Text>

        
    {showImportDeviceModal &&
      <Modal isOpen={isOpen} onClose={handleCloseModal}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Upload your Device Configuration File</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <form
              id="set-password-form"
              onSubmit={(e) => handleImportDevice(e)}
            >
              <FormControl isRequired>
                <FormLabel> File </FormLabel>
                <Input type="password" />
                <FormHelperText>
                  Ensure your file is in the proper format
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
        <Stack spacing={5}>
          {devicePossibleConfigurations.length > 0 ? (
            devicePossibleConfigurations.map((configuration, i) => (
              <ManageDeviceConfigurationCard
                    key={i}
                    actualConfigurations={deviceActualConfigurations}
                    possibleConfigurations={configuration}
                    handleDeviceConfiguration={(deviceConfiguration) => handleDeviceConfiguration(deviceConfiguration)}
              />
            ))
          ) : (
            <p>None available.</p>
          )}
              <Button mr={3} onClick={setImportDevice} colorScheme="red">Import Device Details</Button>
              <Button onClick={handleExportDevice} colorScheme="yellow">Export Device Details</Button>
        </Stack>
      </Container>
    );
}
