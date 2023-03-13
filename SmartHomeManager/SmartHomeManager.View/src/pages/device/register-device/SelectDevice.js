import React from "react";
import { Container, Heading, Stack, Text, Card, CardHeader, CardBody } from "@chakra-ui/react";
import { createSearchParams, useNavigate } from "react-router-dom";

export default function SelectDevice() {
  const dummyDeviceList = [
    {
      deviceBrand: "Xiaomi",
      deviceModel: "Mi Smart Standing Fan 2",
      deviceSerialNumber: "1234",
      deviceWatts: 600,
    },
    {
      deviceBrand: "Xiaomi",
      deviceModel: "Mi Smart LED Bulb",
      deviceSerialNumber: "5678",
      deviceWatts: 100,
    },
    {
      deviceBrand: "NetGear",
      deviceModel: "NetGear Arlo Q",
      deviceSerialNumber: "0123",
      deviceWatts: 300,
    },
  ];

  function handleSelectDevice(e, navigate, device) {
    e.preventDefault();

    navigate({
      pathname: "/registerdevice",
      search: `?${createSearchParams({
        deviceBrand: device.deviceBrand,
        deviceModel: device.deviceModel,
        deviceSerialNumber: device.deviceSerialNumber,
        deviceWatts: device.deviceWatts,
      })}`,
    });

    console.log(device.deviceModel);
  }

  const navigate = useNavigate();

  return (
    <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
      <Heading fontWeight="bold" fontSize="xl" mb={5}>
        Select a Device
      </Heading>

      <Stack spacing={5} minW="full">
        {dummyDeviceList.map((device, i) => {
          return (
            <Card shadow={"md"} as="a" cursor="pointer" key={i} onClick={(event) => handleSelectDevice(event, navigate, device)}>
              <CardHeader>
                <Heading>{device.deviceModel}</Heading>
              </CardHeader>
              <CardBody>
                <Text>{device.deviceBrand}</Text>
              </CardBody>
            </Card>
          );
        })}
      </Stack>
    </Container>
  );
}
