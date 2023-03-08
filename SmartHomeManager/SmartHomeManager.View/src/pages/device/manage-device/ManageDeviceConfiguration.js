import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack, useToast, useDisclosure } from "@chakra-ui/react";
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

    const toast = useToast();


    const { isOpen, onOpen, onClose } = useDisclosure();

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

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


  if (devicePossibleConfigurations.length <= 0 || deviceActualConfigurations.length <= 0) {
    return <Text> Loading </Text>;
  } else
    return (
      <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
        <Text fontWeight="bold" fontSize="xl" mb={5}>
          {`Manage ${deviceName} configurations`}
        </Text>
        <Stack spacing={5}>
          {devicePossibleConfigurations.length > 0 ? (
            devicePossibleConfigurations.map((configuration, i) => (
              <ManageDeviceConfigurationCard
                    key={i}
                    actualConfigurations={deviceActualConfigurations}
                    configurationKey={configuration.configurationKey}
                    configurationValue={configuration.configurationValue}
                    valueMeaning={configuration.valueMeaning}
                    handleDeviceConfiguration={(deviceConfiguration) => handleDeviceConfiguration(deviceConfiguration)}
              />
            ))
          ) : (
            <p>None available.</p>
          )}
        </Stack>
      </Container>
    );
}
