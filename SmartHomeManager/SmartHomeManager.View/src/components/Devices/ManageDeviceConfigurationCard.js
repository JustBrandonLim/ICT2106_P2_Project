import { Box, Checkbox, FormControl, FormLabel, Select, Slider, SliderFilledTrack, SliderMark, SliderThumb, SliderTrack } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";

export default function ManageDeviceConfigurationCard(props) {
    const possibleConfigurations = props.possibleConfigurations
    const actualConfigurations = props.actualConfigurations

    const [applyAll, setApplyAll] = useState(false);
    const [sliderChangingValue, setSliderChangingValue] = useState(parseInt(possibleConfigurations.values[0]));
    const [sliderValue, setSliderValue] = useState(null);

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
        if (actualConfigurationKey.length) setSliderChangingValue(parseInt(actualConfigurationKey[0].configurationValue))
        if (applyAll) handleApplyAll()
        console.log(actualConfigurations)
    }, [actualConfigurations])

    return (
        <FormControl>
            <FormLabel>Configure {possibleConfigurations.name}</FormLabel>
            {!isNaN(possibleConfigurations.values[0]) ?
                <Slider
                    value={sliderChangingValue}
                    min={parseInt(possibleConfigurations.values[0])}
                    max={parseInt(possibleConfigurations.values[possibleConfigurations.values.length - 1])}
                    step={1}
                    onChange={(val) => {setSliderValue(val); setSliderChangingValue(val)}}
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
                }} value={actualConfigurationKey.length > 0 ? actualConfigurationKey[0].configurationValue : 0}>
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
    );
}
