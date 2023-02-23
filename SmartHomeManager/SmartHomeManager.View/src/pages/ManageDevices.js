import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack } from "@chakra-ui/react";
import { createSearchParams, useNavigate } from "react-router-dom";
import ManageDeviceSelectionCard from "components/Devices/ManageDeviceSelectionCard";

export default function ManageDevices() {
  const [devices, setDevices] = useState([]);
  const [assignedDevices, setAssignedDevices] = useState([]);
  const [unassignedDevices, setUnassignedDevices] = useState([]);

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");



    function handleManageSettings(e, navigate, device) {
        e.preventDefault();

        navigate({
            pathname: "/managedevicesettings",
            search: `?${createSearchParams({
                deviceId: device.deviceId,
                deviceName: device.deviceName,
                deviceBrand: device.deviceBrand,
                deviceModel: device.deviceModel,
            })}`,
        });

        console.log(device.deviceModel);
    }

    function handleManageConfiguration(e, navigate, device) {
        e.preventDefault();

        navigate({
            pathname: "/managedeviceconfiguration",
            search: `?${createSearchParams({
                deviceId: device.deviceId,
                deviceName: device.deviceName,
                deviceBrand: device.deviceBrand,
                deviceModel: device.deviceModel,
            })}`,
        });

        console.log(device.deviceModel);
    }
    const navigate = useNavigate();

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
            <Stack spacing={5}>
              {devices.length > 0 ? (
                devices.map((device, i) => (
                    <ManageDeviceSelectionCard
                    handleManageSettings={(event) => handleManageSettings(event, navigate, device)}
                    handleManageConfiguration={(event) => handleManageConfiguration(event, navigate, device)}
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
                    handleManageSettings={(event) => handleManageConfiguration(event, navigate, device)}
                    handleManageConfiguration={(event) => handleManageConfiguration(event, navigate, device)}
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
                    handleManageSettings={(event) => handleManageConfiguration(event, navigate, device)}
                    handleManageConfiguration={(event) => handleManageConfiguration(event, navigate, device)}
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
