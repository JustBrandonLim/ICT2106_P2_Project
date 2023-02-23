import React, { useState } from "react";
import { Heading, Card, CardHeader, CardBody, Text, CardFooter, Button, Select } from "@chakra-ui/react";

export default function ManageDeviceSettingsCard(props) {
    const [deviceSettings, setDeviceSettings] = useState(props.deviceSettings);

    return (
        <Card>
            <CardHeader>
                <Heading size="md">{`Set ${props.deviceSettings}`}</Heading>
            </CardHeader>

            <CardBody>
                <Select value={deviceSettings ? deviceSettings.configurationValue : 0}>
                    {deviceSettings.length > 0 ? (
                        deviceSettings.map((settings, i) => (
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
