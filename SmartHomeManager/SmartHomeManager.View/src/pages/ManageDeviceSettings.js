import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack } from "@chakra-ui/react";
import { useSearchParams } from 'react-router-dom';
import ManageDeviceSettingsCard from "../components/Devices/ManageDeviceSettingsCard";

export default function ManageDeviceSettings() {
  const [searchParams] = useSearchParams();
  const deviceId = searchParams.get('deviceId')
  const deviceName = searchParams.get('deviceName')
  const deviceBrand = searchParams.get('deviceBrand')
  const deviceModel = searchParams.get('deviceModel')
  const [devicePossibleSettings, setDevicePossibleSettings] = useState([]);
  const [deviceActualSettings, setDeviceActualSettings] = useState([]);

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

  useEffect(() => {
      fetch(``)
      .then((response) => response.json())
      .then((data) => {
          setDevicePossibleSettings(data);
      });
  }, []);

  useEffect(() => {
      fetch(``)
          .then((response) => response.json())
          .then((data) => {
              setDeviceActualSettings(data);
          });
  }, []);

  return (
    <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
      <Text fontWeight="bold" fontSize="xl" mb={5}>
              {`Manage ${deviceName} settings`}
      </Text>
            <Stack spacing={5}>
              {devicePossibleSettings.length > 0 ? (
                  devicePossibleSettings.map((configuration, i) => (
                  <ManageDeviceSettingsCard
                    key={i}
                    devicePossibleSettings={devicePossibleSettings}
                  />
                ))
              ) : (
                <p>None available.</p>
              )}
            </Stack>
    </Container>
  );
}
