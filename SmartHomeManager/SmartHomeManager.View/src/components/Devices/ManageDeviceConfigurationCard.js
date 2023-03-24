import { Box, Checkbox, FormControl, FormLabel, Select, Slider, SliderFilledTrack, SliderMark, SliderThumb, SliderTrack } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";

export default function ManageDeviceConfigurationCard(props) {
    const [applyAll, setApplyAll] = useState(false);
    const [sliderValue, setSliderValue] = useState(null);

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

    function applySliderConfig() {
        if (sliderValue && sliderValue != actualConfigurationKey[0].configurationValue) {
            props.handleDeviceConfiguration({
                configurationKey: possibleConfigurations.name,
                configurationValue: sliderValue
            })
            setSliderValue(null);
        }
    }

    useEffect(() => {
        if (applyAll) handleApplyAll()
    }, [applyAll])

    useEffect(() => {
        setActualConfigurationKey(configKey)
        if (applyAll) handleApplyAll()
    }, [actualConfigurations])

    useEffect(() => {

    }, [sliderValue])

    return (
        <FormControl>
            <FormLabel>Configure {possibleConfigurations.name}</FormLabel>
            {!isNaN(possibleConfigurations.values[0]) ?
                <Slider
                    defaultValue={actualConfigurationKey[0].configurationValue}
                    min={parseInt(possibleConfigurations.values[0])}
                    max={parseInt(possibleConfigurations.values[possibleConfigurations.values.length - 1])}
                    step={1}
                    onChange={(val) => setSliderValue(val)}
                    onMouseLeave={() => applySliderConfig()}
                >
                    {possibleConfigurations.values.map((configuration, i) => (
                        <SliderMark key={i} value={parseInt(configuration)} mt={2}>
                            {configuration}
                        </SliderMark>
                    ))}
                    <SliderTrack bg='blue.100'>
                        <Box position='relative' right={10} />
                        <SliderFilledTrack bg='blue' />
                    </SliderTrack>
                    <SliderThumb boxSize={6} />
                </Slider>
                :
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
            }
            <Checkbox fontStyle="italic" mt={4} onChange={(e) => setApplyAll(e.target.checked)}>Apply to all</Checkbox>
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
