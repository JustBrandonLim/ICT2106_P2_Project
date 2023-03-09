import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack, Heading, Card, CardHeader, CardBody, CardFooter, Button, Input, Select, useDisclosure, useToast } from "@chakra-ui/react";
import { useSearchParams } from "react-router-dom";

export default function ManageDeviceSettings() {
  const [searchParams] = useSearchParams();
  const deviceId = searchParams.get("deviceId");
  const [deviceSettings, setDeviceSettings] = useState(null);
  const [deviceTypes, setDeviceTypes] = useState([]);

    const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

    const [newDeviceName, setNewDeviceName] = useState("");
    const [newDevicePassword, setNewDevicePassword] = useState("");
    const [newDeviceType, setNewDeviceType] = useState("");

    const { isOpen, onOpen, onClose } = useDisclosure();
    const toast = useToast()

  useEffect(() => {
      fetchDeviceSettings()
  }, []);

    function fetchDeviceSettings() {
        fetch(`https://localhost:7140/api/ManageDevice/GetDeviceById/${deviceId}`)
            .then((response) => response.json())
            .then((data) => {
                setDeviceSettings(data);
            });
        fetch(`https://localhost:7140/api/RegisterDevice/GetAllDeviceTypes/`)
            .then((response) => response.json())
            .then((data) => {
                setDeviceTypes(data);
            });
        setNewDeviceName(deviceSettings?.deviceName || "");
        setNewDevicePassword(deviceSettings?.devicePassword || "");
        setNewDeviceType(deviceSettings?.deviceTypeName || "");
    }

    function handleDeviceSettings(e) {
        e.preventDefault();
        fetch("https://localhost:7140/api/ManageDevice/ApplyDeviceSettings", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                deviceId: deviceId,
                deviceName: newDeviceName,
                deviceTypeName: newDeviceType,
                devicePassword: newDevicePassword
            }),
        })
            .then((response) => {
                if (response.ok) {
                    toast({
                        title: "Success",
                        description: "Device Settings has been added successfully.",
                        status: "success",
                        duration: 9000,
                        isClosable: true,
                    });
                } else {
                    toast({
                        title: "Error",
                        description: "Device Settings adding failed.",
                        status: "error",
                        duration: 9000,
                        isClosable: true,
                    });
                }
                fetchDeviceSettings();
                onClose();
            });
    }

    const deviceName = deviceSettings?.deviceName || "";
    const deviceTypeName = deviceSettings?.deviceTypeName || "";
    const devicePassword = deviceSettings?.devicePassword || "";

  return (
      <div>
          {deviceSettings ? (
      <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
          <Heading fontWeight="bold" fontSize="3xl" mb={5}>
              {`Manage ${deviceName} settings`}
          </Heading>
      <Stack spacing={5}>
              <Card>
                  <CardBody>
                      <Heading size="md">Device Name</Heading>
                      <Input onChange={(e) => setNewDeviceName(e.target.value)} defaultValue={deviceName} />

                      <Heading mt={5} size="md">Device Password</Heading>
                      <Input onChange={(e) => setNewDevicePassword(e.target.value)} defaultValue={devicePassword} />

                      <Heading mt={5} size="md">Device Type</Heading>
                              <Select onChange={(e) => setNewDeviceType(e.target.value)} defaultValue={deviceTypeName ? deviceTypeName : null}>
                          {deviceTypes.length > 0 ? (
                              deviceTypes.map((types, i) => (
                                  <option
                                      key={i}
                                      value={types}
                                  >
                                      {types}
                                  </option>
                              ))
                          ) : (
                              <p>None available.</p>
                          )}
                      </Select>
                      </CardBody>
                  <CardFooter flex="row" justifyContent="flex-end">
                      <Button onClick={handleDeviceSettings} colorScheme="green">Apply Changes</Button>
                  </CardFooter>
              </Card>
      </Stack>
      </Container>
              )
      : (
        <div>Loading...</div>
    )
}
  </div >
  );
}
