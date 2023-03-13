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

    const [selectedConfigurationValue, setSelectedConfigurationValue] = useState(actualConfigurationKey[0]);

    console.log(selectedConfigurationValue)

    return (
        <Card>
            <CardHeader>
                <Heading size="md">{`Configure ${props.configurationKey}`}</Heading>
            </CardHeader>

            <CardBody>
                <Select onChange={(e) => { setSelectedConfigurationValue(e.target.value) }} defaultValue={actualConfigurationKey[0] ? actualConfigurationKey[0].configurationValue : 0}>
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
                <Button onClick={() => {
                    props.handleDeviceConfiguration({
                        configurationKey: props.configurationKey,
                        configurationValue: selectedConfigurationValue
                    })
                }} colorScheme="green">Apply Changes</Button>
            </CardFooter>
        </Card>
    );
}
