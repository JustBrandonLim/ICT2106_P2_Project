import React, { useEffect, useState } from "react";
import { Container, Text, Tabs, TabList, Tab, TabPanels, TabPanel, Stack, Heading, Card, CardHeader, CardBody, CardFooter, Button, Input, Select, useDisclosure, useToast, FormControl, Checkbox, FormLabel, Box, Flex } from "@chakra-ui/react";
import { useSearchParams } from "react-router-dom";

export default function ManageDeviceSettings() {
    const tempId = "11111111-1111-1111-1111-111111111111";
    const [searchParams] = useSearchParams();
    const [deviceSettings, setDeviceSettings] = useState(null);
    const [deviceTypes, setDeviceTypes] = useState([]);

    const [accountId, setAccountId] = useState("");
    const [deviceId, setDeviceId] = useState("");

    const [newDeviceName, setNewDeviceName] = useState("");
    const [newDevicePassword, setNewDevicePassword] = useState("");
    const [newDeviceType, setNewDeviceType] = useState("");
    const [applyAll, setApplyAll] = useState(false);

    const toast = useToast()

    useEffect(() => {
        const accId = JSON.parse(localStorage.getItem('accountId'));
        const devId = searchParams.get("deviceId");
        if (accId) setAccountId(accId)
        else setAccountId(tempId);

        if (devId) setDeviceId(devId);
    }, [])

    useEffect(() => {
        if (deviceId) fetchDeviceSettings();
    }, [deviceId])

    function fetchDeviceSettings() {
        fetch(`https://localhost:7140/api/ManageDevice/GetDeviceById/${deviceId}`)
            .then((response) => response.json())
            .then((data) => {
                setDeviceSettings(data);
                setNewDeviceName(data.deviceName);
                setNewDevicePassword(data.devicePassword || "");
                setNewDeviceType(data.deviceTypeName);
            });
        fetch(`https://localhost:7140/api/RegisterDevice/GetAllDeviceTypes/`)
            .then((response) => response.json())
            .then((data) => {
                setDeviceTypes(data);
            });
    }

    function handleDeviceSettings(e) {
        e.preventDefault();

        if (newDevicePassword == "") {
            setNewDevicePassword("")
        }

        fetch("https://localhost:7140/api/ManageDevice/ApplyDeviceMetadata", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                deviceId: deviceId,
                deviceName: newDeviceName,
                deviceTypeName: newDeviceType,
                devicePassword: newDevicePassword
            }),
        })
            .then((response) => {
                if (response.ok) {
                    if (!applyAll) {
                        toast({
                            title: "Success",
                            description: "Device Settings has been added successfully.",
                            status: "success",
                            duration: 9000,
                            isClosable: true,
                        });
                    }
                    else applySettingsToAll() // call api
                } else {
                    toast({
                        title: "Error",
                        description: "Device Settings adding failed.",
                        status: "error",
                        duration: 9000,
                        isClosable: true,
                    });
                }
                fetchDeviceSettings();
            });
    }

    function applySettingsToAll() {
        // remove after api implemented
        toast({
            title: "Success",
            description: `Device Settings has been applied to all ${newDeviceType} successfully.`,
            status: "success",
            duration: 9000,
            isClosable: true,
        });
        return
        fetch("https://localhost:7140/api/ManageDevice/Apply...", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                deviceId: deviceId,
                deviceTypeName: newDeviceType
            }),
        })
            .then((response) => {
                if (response.ok) {
                    toast({
                        title: "Success",
                        description: `Device Settings has been applied to all ${newDeviceType} successfully.`,
                        status: "success",
                        duration: 9000,
                        isClosable: true,
                    });
                } else {
                    toast({
                        title: "Error",
                        description: `Failed to apply Device Settings to all ${newDeviceType}.`,
                        status: "error",
                        duration: 9000,
                        isClosable: true,
                    });
                }
            });
    }

    const deviceName = deviceSettings?.deviceName || "";
    const deviceTypeName = deviceSettings?.deviceTypeName || "";
    const devicePassword = deviceSettings?.devicePassword || "";

    return (
        <div>
            {deviceSettings ? (
                <Container mt={5}>

                    <Flex
                        align={'center'}
                        justify={'center'}>
                        <Stack spacing={8} mx={'auto'} minW={'lg'} maxW={'xl'} py={12} px={6}>
                            <Stack align={'center'}>
                                <Heading fontSize={'4xl'}>{deviceName} Settings</Heading>
                            </Stack>
                            <Box
                                rounded={'lg'}
                                bg='white'
                                boxShadow={'lg'}
                                p={8}>
                                <Stack spacing={4}>
                                    <FormControl>
                                        <FormLabel>Device Name</FormLabel>
                                        <Input onChange={(e) => setNewDeviceName(e.target.value)} defaultValue={deviceName} />
                                    </FormControl>
                                    <FormControl>
                                        <FormLabel>Device Password</FormLabel>
                                        <Input onChange={(e) => setNewDevicePassword(e.target.value)} defaultValue={devicePassword} />
                                    </FormControl>
                                    <FormControl>
                                        <FormLabel>Device Type</FormLabel>
                                        <Select onChange={(e) => setNewDeviceType(e.target.value)} defaultValue={deviceTypeName ? deviceTypeName : null}>
                                            {deviceTypes.length > 0 ? (
                                                deviceTypes.map((types, i) => (
                                                    <option
                                                        key={i}
                                                        value={types}
                                                    >
                                                        {types}
                                                    </option>
                                                ))
                                            ) : (
                                                <p>None available.</p>
                                            )}
                                        </Select>
                                    </FormControl>
                                    <Stack spacing={10}>
                                        <Stack
                                            direction={{ base: 'column', sm: 'row' }}
                                            align={'start'}
                                            justify={'space-between'}>
                                            <Checkbox fontStyle="italic" onChange={(e) => setApplyAll(e.target.checked)}>Apply to all</Checkbox>
                                        </Stack>
                                        <Button
                                            bg={'blue.400'}
                                            color={'white'}
                                            _hover={{
                                                bg: 'blue.500',
                                            }}
                                            onClick={handleDeviceSettings}>
                                            Apply Changes
                                        </Button>
                                    </Stack>
                                </Stack>
                            </Box>
                        </Stack>
                    </Flex>
                    {/* <Heading fontWeight="bold" fontSize="3xl" mb={5}>
                        {`Manage ${deviceName} settings`}
                    </Heading>
                    <Stack spacing={5}>
                        <Card>
                            <CardBody>
                                <Heading size="md">Device Name</Heading>
                                <Input onChange={(e) => setNewDeviceName(e.target.value)} defaultValue={deviceName} />

                                <Heading mt={5} size="md">Device Password</Heading>
                                <Input onChange={(e) => setNewDevicePassword(e.target.value)} defaultValue={devicePassword} />

                                <Heading mt={5} size="md">Device Type</Heading>
                                <Select onChange={(e) => setNewDeviceType(e.target.value)} defaultValue={deviceTypeName ? deviceTypeName : null}>
                                    {deviceTypes.length > 0 ? (
                                        deviceTypes.map((types, i) => (
                                            <option
                                                key={i}
                                                value={types}
                                            >
                                                {types}
                                            </option>
                                        ))
                                    ) : (
                                        <p>None available.</p>
                                    )}
                                </Select>
                            </CardBody>
                            <CardFooter flex="row" justifyContent="flex-end">
                                <Button onClick={handleDeviceSettings} colorScheme="green">Apply Changes</Button>
                            </CardFooter>
                        </Card>
                    </Stack> */}
                </Container>
            )
                : (
                    <div>Loading...</div>
                )
            }
        </div >
    );
}
