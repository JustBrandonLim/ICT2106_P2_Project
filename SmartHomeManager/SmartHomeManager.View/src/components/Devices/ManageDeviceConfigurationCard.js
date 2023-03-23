import { Checkbox, FormControl, FormLabel, Select } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";

export default function ManageDeviceConfigurationCard(props) {
    const [applyAll, setApplyAll] = useState(false);

    const possibleConfigurations = props.possibleConfigurations
    const actualConfigurations = props.actualConfigurations
    let configKey = actualConfigurations.filter((actualConfiguration) => {
        return actualConfiguration.configurationKey == possibleConfigurations.name;
    })
    const [actualConfigurationKey, setActualConfigurationKey] = useState(configKey);

    function handleApplyAll() {
        props.handleDeviceApplyAll({
            configurationKey: possibleConfigurations.name,
            configurationValue: actualConfigurationKey[0].configurationValue
        })
    }

    useEffect(() => {
        if (applyAll) handleApplyAll()
    }, [applyAll])

    useEffect(() => {
        setActualConfigurationKey(configKey)
        if (applyAll) handleApplyAll()
    }, [actualConfigurations])

    return (
        <FormControl>
            <FormLabel>Configure {possibleConfigurations.name}</FormLabel>
            <Select onChange={(e) => {
                // const selectedIndex = possibleConfigurations.values.findIndex((value) => value === e.target.value);
                props.handleDeviceConfiguration({
                    configurationKey: possibleConfigurations.name,
                    configurationValue: e.target.value
                })
            }} defaultValue={actualConfigurationKey.length > 0 ? actualConfigurationKey[0].configurationValue : 0}>
                {possibleConfigurations.values.length > 0 ? (
                    possibleConfigurations.values.map((configuration, i) => (
                        <option
                            key={i}
                            value={i}
                        >
                            {configuration}
                        </option>
                    ))
                ) : (
                    <p>None available.</p>
                )}
            </Select>
            <Checkbox fontStyle="italic" onChange={(e) => setApplyAll(e.target.checked)}>Apply to all</Checkbox>
        </FormControl>
        // <Card>
        //     <CardHeader>
        //         <Heading size="md">{`Configure ${possibleConfigurations.name}`}</Heading>
        //     </CardHeader>

        //     <CardBody>
        //         <Select onChange={(e) => {
        //             const selectedIndex = possibleConfigurations.values.findIndex((value) => value === e.target.value);
        //             props.handleDeviceConfiguration({
        //                 configurationKey: possibleConfigurations.name,
        //                 configurationValue: selectedIndex
        //             })
        //         }} defaultValue={actualConfigurationKey[0] ? actualConfigurationKey[0].configurationValue : 0}>
        //             {possibleConfigurations.values.length > 0 ? (
        //                 possibleConfigurations.values.map((configuration, i) => (
        //                     <option
        //                         key={i}
        //                         value={configuration}
        //                     >
        //                         {configuration}
        //                     </option>
        //                 ))
        //             ) : (
        //                 <p>None available.</p>
        //             )}
        //         </Select>
        //     </CardBody>
        //     <CardFooter flex="row" justifyContent="flex-end">
        //     </CardFooter>
        // </Card>
    );
}
