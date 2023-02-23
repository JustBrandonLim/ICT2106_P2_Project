import React, { useState } from "react";
import { Heading, Card, CardHeader, CardBody, Text, CardFooter, Button, Select } from "@chakra-ui/react";

export default function ManageDeviceSettingsCard(props) {
    const [devicePossibleSettings, setDevicePossibleSettings] = useState(props.devicePossibleSettings);

    return (
        <Card>
            <CardHeader>
                <Heading size="md">{`Configure ${props.configurationKey}`}</Heading>
            </CardHeader>

            <CardBody>
                <Select value={devicePossibleSettings ? devicePossibleSettings.configurationValue : 0}>
                    {devicePossibleSettings.length > 0 ? (
                        devicePossibleSettings.map((settings, i) => (
                            <option
                                key={i}
                                value={settings}
                            >
                                test
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
    );
}
