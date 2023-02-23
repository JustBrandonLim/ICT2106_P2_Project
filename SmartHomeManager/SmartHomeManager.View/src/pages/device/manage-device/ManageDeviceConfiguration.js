import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack } from "@chakra-ui/react";
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

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

  useEffect(() => {
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
  }, []);

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
              />
            ))
          ) : (
            <p>None available.</p>
          )}
        </Stack>
      </Container>
    );
}
