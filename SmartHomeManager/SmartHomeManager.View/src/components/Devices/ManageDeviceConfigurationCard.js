import React, { useState } from "react";
import { Heading, Card, CardHeader, CardBody, Text, CardFooter, Button, Select } from "@chakra-ui/react";

export default function ManageDeviceConfigurationCard(props) {
    const valueMeaning = props.valueMeaning.split(",")
    const possibleConfigurations = props.configurationValue.split(",")
    const [configurationValueMeaning, setConfigurationValueMeaning] = useState(valueMeaning);
    const actualConfigurations = props.actualConfigurations
    const [actualConfigurationKey, setActualConfigurationKey] = useState
        (actualConfigurations.filter((actualConfiguration) => {
            return actualConfiguration.configurationKey == props.configurationKey;
        }));

    return (
        <Card>
            <CardHeader>
                <Heading size="md">{`Configure ${props.configurationKey}`}</Heading>
            </CardHeader>

            <CardBody>
                <Select value={actualConfigurationKey ? actualConfigurationKey.configurationValue : 0}>
                    {possibleConfigurations.length > 0 ? (
                        possibleConfigurations.map((configuration, i) => (
                            <option
                                key={i}
                                value={configuration}
                            >
                                {configurationValueMeaning[i]}
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
