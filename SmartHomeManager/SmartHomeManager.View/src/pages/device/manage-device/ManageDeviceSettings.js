import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack, Heading, Card, CardHeader, CardBody, CardFooter, Button, Input, Select } from "@chakra-ui/react";
import { useSearchParams } from "react-router-dom";

export default function ManageDeviceSettings() {
  const [searchParams] = useSearchParams();
  const deviceId = searchParams.get("deviceId");
  const [deviceSettings, setDeviceSettings] = useState([]);
  const [deviceTypes, setDeviceTypes] = useState([]);

  const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");

  useEffect(() => {
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
  }, []);

    const { deviceName, deviceBrand, deviceModel, deviceSerialNumber, deviceType, devicePassword  } = deviceSettings;

  return (
    <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
          <Heading fontWeight="bold" fontSize="3xl" mb={5}>
              {`Manage ${deviceName} settings`}
          </Heading>
      <Stack spacing={5}>
              <Card>
                  <CardBody>
                      <Heading size="md">Device Name</Heading>
                      <Input defaultValue={deviceName} />

                      <Heading mt={5} size="md">Device Password</Heading>
                      <Input defaultValue={devicePassword} />

                      <Heading mt={5} size="md">Device Serial Number</Heading>
                      <Input defaultValue={deviceSerialNumber} />

                      <Heading mt={5} size="md">Device Brand</Heading>
                      <Input defaultValue={deviceBrand} />

                      <Heading mt={5} size="md">Device Model</Heading>
                      <Input defaultValue={deviceModel} />

                      <Heading mt={5} size="md">Device Type</Heading>
                      <Select value={deviceType ? deviceType : null}>
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
                      <Button colorScheme="green">Apply Changes</Button>
                  </CardFooter>
              </Card>
      </Stack>
    </Container>
  );
}
