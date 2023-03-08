import React, { useState } from "react";
import { Heading, Card, CardHeader, CardBody, Text, CardFooter, Button, Input } from "@chakra-ui/react";

export default function ManageDeviceSettingsCard(props) {
    const [deviceSettings, setDeviceSettings] = useState(props.deviceSettings);

    console.log(props)

    return (
        <Card>
            <CardHeader>
                <Heading size="xl">{`Modify ${props.deviceName} Settings`}</Heading>
            </CardHeader>

            <CardBody>
                <Input mt={5}>{props.deviceSerialNumber}</Input>
                <Input mt={5}>{props.deviceBrand}</Input>
                <Input mt={5}>{props.deviceModel}</Input>
                <Input mt={5}>{props.deviceType}</Input>
                <Input mt={5}>{props.deviceName}</Input>
                <Input mt={5}>{props.devicePassword}</Input>
            </CardBody>
            <CardFooter flex="row" justifyContent="flex-end">
                <Button colorScheme="green">Apply Changes</Button>
            </CardFooter>
        </Card>
    );
}