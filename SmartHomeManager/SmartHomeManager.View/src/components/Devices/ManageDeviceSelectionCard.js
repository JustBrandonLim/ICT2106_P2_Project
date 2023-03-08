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
              {props.handleSetPassword && <Button mr={3} onClick={props.handleSetPassword}  colorScheme="blue">Set Password</Button>}
              <Button mr={3} onClick={props.handleManageSettings}  colorScheme="blue">Manage Settings</Button>
              <Button onClick={props.handleManageConfiguration} colorScheme="green">Manage Configuration</Button>
      </CardFooter>
    </Card>
  );
}
