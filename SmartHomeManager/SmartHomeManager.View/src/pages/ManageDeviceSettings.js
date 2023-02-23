import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack } from "@chakra-ui/react";
import { useSearchParams } from 'react-router-dom';
import ManageDeviceSettingsCard from "../components/Devices/ManageDeviceSettingsCard";

export default function ManageDeviceSettings() {
  const [searchParams] = useSearchParams();
  const deviceId = searchParams.get('deviceId')
  const [deviceSettings, setDeviceSettings] = useState([]);

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

    console.log(deviceSettings)

  useEffect(() => {
      fetch(`https://localhost:7140/api/ManageDevice/GetDeviceById/${deviceId}`)
          .then((response) => response.json())
          .then((data) => {
              setDeviceSettings(data);
          });
  }, []);

  return (
    <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
      <Text fontWeight="bold" fontSize="xl" mb={5}>
              {`Manage ${deviceSettings} settings`}
      </Text>
            <Stack spacing={5}>
              {deviceSettings.length > 0 ? (
                  deviceSettings.map((configuration, i) => (
                  <ManageDeviceSettingsCard
                    key={i}
                    deviceSettings={deviceSettings}
                  />
                ))
              ) : (
                <p>None available.</p>
              )}
            </Stack>
    </Container>
  );
}
