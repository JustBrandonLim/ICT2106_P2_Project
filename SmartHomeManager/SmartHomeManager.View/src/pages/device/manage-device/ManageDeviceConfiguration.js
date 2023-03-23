import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Button, Stack, useToast, useDisclosure, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, FormControl, FormLabel, Input, FormHelperText, Flex, Heading, Box, Checkbox } from "@chakra-ui/react";
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

  // for import device
  const [showImportDeviceModal, setShowImportDeviceModal] = useState(false);
  const [selectedFile, setSelectedFile] = useState(null);

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

  function applySettingsToAll({ configurationKey, configurationValue }) {
    let deviceType = deviceName.split(" ")[1]
    fetch("https://localhost:7140/api/ManageDevice/ApplyDeviceConfigurationsSameType", {
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
            description: `Device Settings has been applied to all ${deviceType} successfully.`,
            status: "success",
            duration: 9000,
            isClosable: true,
          });
        } else {
          toast({
            title: "Error",
            description: `Failed to apply Device Settings to all ${deviceType}.`,
            status: "error",
            duration: 9000,
            isClosable: true,
          });
        }
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
        downloadJson(data, `${deviceName}_config.json`);
      })
      .catch((error) => {
        console.error('Error exporting device:', error);
      });
  }

  function downloadJson(data, fileName) {
    const jsonData = JSON.stringify(data);
    const blob = new Blob([jsonData], { type: "application/json" });
    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.href = url;
    link.download = fileName;
    link.click();
  }

  function setImportDevice(e) {
    e.preventDefault();
    setShowImportDeviceModal(true);
    onOpen();
  }


  function handleImportDevice(e) {
    e.preventDefault();
    const reader = new FileReader();
    reader.onload = (event) => {
      const deviceConfigurationJson = event.target.result;
      fetch(`https://localhost:7140/api/ManageDevice/ImportDeviceConfigurations/${deviceId}/${deviceConfigurationJson}`, {
        method: "POST",
        body: JSON.stringify({
          deviceBrand: deviceBrand,
          deviceModel: deviceModel,
          deviceConfigurationJson: deviceConfigurationJson,
        }),
        headers: {
          "Content-Type": "application/json",
        },
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
        .finally(() => {
          setSelectedFile(null);
          handleCloseModal();
          fetchDeviceConfigurations();
        });
    };
    reader.readAsText(selectedFile);
  }



  // if (devicePossibleConfigurations.length <= 0 && deviceActualConfigurations.length <= 0) {
  //   return <Text> Loading </Text>;
  // } else
  if (devicePossibleConfigurations.length > 0 && deviceActualConfigurations.length > 0)
    return (
      <Container mt={5}>
        <Flex
          align={'center'}
          justify={'center'}>
          <Stack spacing={8} mx={'auto'} minW={'xl'} py={12} px={6}>
            <Stack align={'center'}>
              <Heading fontSize={'4xl'}>{deviceName} Configurations</Heading>
            </Stack>
            <Box
              rounded={'lg'}
              bg='white'
              boxShadow={'lg'}
              p={8}>
              <Stack spacing={4}>
                {devicePossibleConfigurations.map((configuration, i) => (
                  <ManageDeviceConfigurationCard
                    key={i}
                    actualConfigurations={deviceActualConfigurations}
                    possibleConfigurations={configuration}
                    handleDeviceConfiguration={(deviceConfiguration) => handleDeviceConfiguration(deviceConfiguration)}
                    handleDeviceApplyAll={(deviceConfiguration) => applySettingsToAll(deviceConfiguration)}
                  />
                ))}
                <Stack pt={5} spacing={6} direction={['column', 'row']} justifyContent="space-between">
                  <Button
                    bg={'blue.400'}
                    color={'white'}
                    _hover={{
                      bg: 'blue.500',
                    }}
                    onClick={setImportDevice}>
                    Import Configurations
                  </Button>
                  <Button
                    bg={'green.400'}
                    color={'white'}
                    _hover={{
                      bg: 'green.500',
                    }}
                    onClick={handleExportDevice}>
                    Export Configurations
                  </Button>
                </Stack>
              </Stack>
            </Box>
          </Stack>
        </Flex>

        {showImportDeviceModal &&
          <Modal isOpen={isOpen} onClose={handleCloseModal}>
            <ModalOverlay />
            <ModalContent>
              <ModalHeader>Upload your Device Configuration File</ModalHeader>
              <ModalCloseButton />
              <ModalBody>
                <form
                  id="import-device-form"
                  onSubmit={(e) => handleImportDevice(e)}
                >
                  <FormControl isRequired>
                    <FormLabel> File </FormLabel>
                    <Input type="file" onChange={(e) => setSelectedFile(e.target.files[0])} sx={{ border: 'none', boxShadow: 'none', '&:focus': { boxShadow: 'none' } }} />
                    <FormHelperText>
                      Ensure your file is in the proper format
                    </FormHelperText>
                  </FormControl>
                </form>
              </ModalBody>
              <ModalFooter>
                <Button type="submit" form="import-device-form" colorScheme='green' mr={3}>
                  Import Device Configuration
                </Button>
              </ModalFooter>
            </ModalContent>
          </Modal>
        }
        {/* <Stack spacing={5}>
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
          <Button mr={3} onClick={setImportDevice} colorScheme="red">Import Device Configurations</Button>
          <Button onClick={handleExportDevice} colorScheme="yellow">Export Device Configurations</Button>
        </Stack> */}
      </Container>
    );

  else return <Text> Loading </Text>;
}
