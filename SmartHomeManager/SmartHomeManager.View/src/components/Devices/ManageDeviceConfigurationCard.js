import React, { useState } from "react";
import { Heading, Card, CardHeader, CardBody, Text, CardFooter, Button, Select } from "@chakra-ui/react";

export default function ManageDeviceConfigurationCard(props) {
    const possibleConfigurations = props.possibleConfigurations
    const actualConfigurations = props.actualConfigurations
    const [actualConfigurationKey, setActualConfigurationKey] = useState
        (actualConfigurations.filter((actualConfiguration) => {
            return actualConfiguration.configurationKey == props.possibleConfigurations.name;
        }));

    return (
        <Card>
            <CardHeader>
                <Heading size="md">{`Configure ${possibleConfigurations.name}`}</Heading>
            </CardHeader>

            <CardBody>
                <Select onChange={(e) => {
                    const selectedIndex = possibleConfigurations.values.findIndex((value) => value === e.target.value);
                    props.handleDeviceConfiguration({
                        configurationKey: possibleConfigurations.name,
                        configurationValue: selectedIndex
                    })
                }} defaultValue={actualConfigurationKey[0] ? actualConfigurationKey[0].configurationValue : 0}>
                    {possibleConfigurations.values.length > 0 ? (
                        possibleConfigurations.values.map((configuration, i) => (
                            <option
                                key={i}
                                value={configuration}
                            >
                                {configuration}
                            </option>
                        ))
                    ) : (
                        <p>None available.</p>
                    )}
                </Select>
            </CardBody>
            <CardFooter flex="row" justifyContent="flex-end">
            </CardFooter>
        </Card>
    );
}
