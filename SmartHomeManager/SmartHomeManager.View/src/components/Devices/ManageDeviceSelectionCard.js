import React from "react";
import { Heading, Card, CardHeader, CardBody, Text, CardFooter, Button } from "@chakra-ui/react";

export default function ManageDeviceSelectionCard(props) {
  return (
    <Card>
      <CardHeader>
        <Heading size="xl">{props.deviceName}</Heading>
      </CardHeader>

      <CardBody>
        <Text mt={5}>{props.deviceSerialNumber}</Text>
        <Text mt={5}>{props.deviceBrand}</Text>
        <Text mt={5}>{props.deviceModel}</Text>
      </CardBody>
      <CardFooter flex="row" justifyContent="flex-end">
        <Button colorScheme="green">Manage Device</Button>
      </CardFooter>
    </Card>
  );
}
