import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel } from "@chakra-ui/react";

export default function ManageDevices() {
  const [devices, setDevices] = useState([]);
  const [assignedDevices, setAssignedDevices] = useState([]);
  const [unassignedDevices, setUnassignedDevices] = useState([]);

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

  useEffect(() => {
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
  }, []);

  return (
    <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
      <Text fontWeight="bold" fontSize="xl" mb={5}>
        Manage Devices
      </Text>

      <Tabs w="full">
        <TabList>
          <Tab>All</Tab>
          <Tab>Assigned</Tab>
          <Tab>Unassigned</Tab>
        </TabList>
        <TabPanels>
          <TabPanel>
            {devices.length > 0 ? (
              devices.map((device, i) => (
                <p key={i}>
                  {device.deviceName}, {device.deviceId}
                </p>
              ))
            ) : (
              <p>None available.</p>
            )}
          </TabPanel>
          <TabPanel>
            {assignedDevices.length > 0 ? (
              assignedDevices.map((device, i) => (
                <p key={i}>
                  {device.deviceName}, {device.deviceId}
                </p>
              ))
            ) : (
              <p>None available.</p>
            )}
          </TabPanel>
          <TabPanel>
            {unassignedDevices.length > 0 ? (
              unassignedDevices.map((device, i) => (
                <p key={i}>
                  {device.deviceName}, {device.deviceId}
                </p>
              ))
            ) : (
              <p>None available.</p>
            )}
          </TabPanel>
        </TabPanels>
      </Tabs>
    </Container>
  );
}
