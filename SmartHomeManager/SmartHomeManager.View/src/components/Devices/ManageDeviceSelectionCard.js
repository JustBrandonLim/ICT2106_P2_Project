import React from "react";
import { Heading, Card, CardHeader, CardBody, Text, CardFooter, Button, Flex, Box } from "@chakra-ui/react";
import { LockIcon, UnlockIcon } from "@chakra-ui/icons";

export default function ManageDeviceSelectionCard(props) {
  return (
    <Card>
      <CardHeader>
        {!props.handleSetPassword ?
          <Flex gap={2}>
            <Heading size="xl">{props.deviceName}</Heading>
            <LockIcon mt={2} color="gray.400" />
          </Flex>
          :
          <Flex gap={2} justifyContent="space-between">
            <Heading size="xl">{props.deviceName}</Heading>
            <Box title="Lock Device" boxShadow='lg' p={2} rounded='md' bg='white' cursor="pointer" onClick={props.handleSetPassword}>
              <UnlockIcon mt={2} color="blue" />
            </Box>
          </Flex>
        }
      </CardHeader>

      <CardBody>
        <Text mt={2}>{props.deviceSerialNumber}</Text>
        <Text mt={2}>{props.deviceBrand}</Text>
        <Text mt={2}>{props.deviceModel}</Text>
      </CardBody>
      <CardFooter flex="row" justifyContent="flex-end">
        {/* {props.handleSetPassword && <Button mr={3} onClick={props.handleSetPassword} colorScheme="blue">Set Password</Button>} */}
        <Button mr={3} onClick={props.handleManageSettings} colorScheme="blue">Manage Settings</Button>
        <Button onClick={props.handleManageConfiguration} colorScheme="blue">Manage Configuration</Button>
      </CardFooter>
    </Card>
  );
}
