import {
    Container,
    Stack,
    Text,
    FormControl,
    FormLabel,
    Input,
    Select,
    Button,
    useDisclosure,
    Modal,
    ModalOverlay,
    ModalBody,
    ModalContent,
    ModalHeader,
    ModalCloseButton,
    ModalFooter,
    useToast,
    Box,
  } from "@chakra-ui/react";
  import React, { useEffect, useState } from "react";
  import { useSearchParams } from "react-router-dom";
  
  export default function OnboardDevice() {
    const [deviceTypeNames, setDeviceTypeNames] = useState([]);
  
    const [newDeviceTypeName, setNewDeviceTypeName] = useState("");
  
    const [searchParams, setSearchParams] = useSearchParams();
  
    const [deviceName, setDeviceName] = useState("");
    const [deviceBrand, setDeviceBrand] = useState(searchParams.get("deviceBrand"));
    const [deviceModel, setDeviceModel] = useState(searchParams.get("deviceModel"));
    const [deviceWatts, setDeviceWatts] = useState(searchParams.get("deviceWatts"));
    const [deviceTypeName, setDeviceTypeName] = useState("");
    const [deviceSerialNumber, setSerialNumber] = useState(searchParams.get("deviceSerialNumber"));
  
    const { isOpen, onOpen, onClose } = useDisclosure();
  
    const toast = useToast();
  
    function fetchDeviceTypes() {
      fetch("https://localhost:7140/api/RegisterDevice/GetAllDeviceTypes/")
        .then((response) => response.json())
        .then((data) => setDeviceTypeNames(data));
    }
  
    useEffect(() => {
      fetchDeviceTypes();
    }, []);
  
    function handleAddNewDeviceType(e) {
      e.preventDefault();
  
      fetch("https://localhost:7140/api/RegisterDevice/AddDeviceType/", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ deviceTypeName: newDeviceTypeName }),
      }).then((response) => {
        if (response.ok) {
          toast({
            title: "Success",
            description: "Device Type has been added successfully.",
            status: "success",
            duration: 9000,
            isClosable: true,
          });
        } else {
          toast({
            title: "Error",
            description: "Device Type adding failed.",
            status: "error",
            duration: 9000,
            isClosable: true,
          });
        }
        setDeviceTypeNames([]);
        fetchDeviceTypes();
        onClose();
      });
    }
  
    function handleRegisterDevice(e) {
      e.preventDefault();
  
      fetch("https://localhost:7140/api/RegisterDevice/RegisterDevice/", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          deviceName: deviceName,
          deviceBrand: deviceBrand,
          deviceModel: deviceModel,
          deviceWatts: deviceWatts,
          deviceTypeName: deviceTypeName,
          deviceSerialNumber: deviceSerialNumber,
          accountId: "11111111-1111-1111-1111-111111111111",
        }),
      }).then((response) => {
        if (response.ok) {
          toast({
            title: "Success",
            description: "Device has been registered successfully.",
            status: "success",
            duration: 9000,
            isClosable: true,
          });
        } else {
          toast({
            title: "Error",
            description: "Device registration failed.",
            status: "error",
            duration: 9000,
            isClosable: true,
          });
        }
        setDeviceTypeNames([]);
        fetchDeviceTypes();
        onClose();
      });
    }
  
    return (
      <Container>
      <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
        <Text fontWeight="bold" fontSize="xl">
          Register Device 1
        </Text>
  
        <Stack spacing={5} minW="full">
          <FormControl isRequired>
            <FormLabel>Device Name</FormLabel>
            <Input onChange={(e) => setDeviceName(e.target.value)} type="text" placeholder="John's Smart Fan" />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Brand</FormLabel>
            <Input isDisabled type="text" placeholder={deviceBrand} value={deviceBrand} />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Model</FormLabel>
            <Input isDisabled type="text" placeholder={deviceModel} value={deviceModel} />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Type</FormLabel>
            <Select onChange={(e) => setDeviceTypeName(e.target.value)} placeholder="Please a select a device type">
              {deviceTypeNames
                ? deviceTypeNames.map((item, i) => {
                    return (
                      <option key={i} value={item}>
                        {item}
                      </option>
                    );
                  })
                : null}
            </Select>
          </FormControl>
          <Stack direction="row" spacing={5} justifyContent="flex-end">
            <Button colorScheme="blue" onClick={onOpen}>
              Add Device Type
            </Button>
            <Button colorScheme="green" onClick={handleRegisterDevice}>
              Register Device 1
            </Button>
          </Stack>
        </Stack>
  
        <Modal blockScrollOnMount={false} isOpen={isOpen} onClose={onClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Add Device Type</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <FormControl isRequired>
                <FormLabel>Device Type</FormLabel>
                <Input onChange={(e) => setNewDeviceTypeName(e.target.value)} type="text" placeholder="Smart Fan" />
              </FormControl>
            </ModalBody>
            <ModalFooter>
              <Button variant="ghost" mr={3} onClick={onClose}>
                Close
              </Button>
              <Button colorScheme="green" onClick={handleAddNewDeviceType}>
                Add Device Type
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </Container>
      <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
        <Text fontWeight="bold" fontSize="xl">
          Register Device 2
        </Text>
  
        <Stack spacing={5} minW="full">
          <FormControl isRequired>
            <FormLabel>Device Name</FormLabel>
            <Input onChange={(e) => setDeviceName(e.target.value)} type="text" placeholder="John's Smart Fan" />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Brand</FormLabel>
            <Input isDisabled type="text" placeholder={deviceBrand} value={deviceBrand} />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Model</FormLabel>
            <Input isDisabled type="text" placeholder={deviceModel} value={deviceModel} />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Type</FormLabel>
            <Select onChange={(e) => setDeviceTypeName(e.target.value)} placeholder="Please a select a device type">
              {deviceTypeNames
                ? deviceTypeNames.map((item, i) => {
                    return (
                      <option key={i} value={item}>
                        {item}
                      </option>
                    );
                  })
                : null}
            </Select>
          </FormControl>
          <Stack direction="row" spacing={5} justifyContent="flex-end">
            <Button colorScheme="blue" onClick={onOpen}>
              Add Device Type
            </Button>
            <Button colorScheme="green" onClick={handleRegisterDevice}>
              Register Device 2
            </Button>
          </Stack>
        </Stack>
  
        <Modal blockScrollOnMount={false} isOpen={isOpen} onClose={onClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Add Device Type</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <FormControl isRequired>
                <FormLabel>Device Type</FormLabel>
                <Input onChange={(e) => setNewDeviceTypeName(e.target.value)} type="text" placeholder="Smart Fan" />
              </FormControl>
            </ModalBody>
            <ModalFooter>
              <Button variant="ghost" mr={3} onClick={onClose}>
                Close
              </Button>
              <Button colorScheme="green" onClick={handleAddNewDeviceType}>
                Add Device Type
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </Container>
      <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
        <Text fontWeight="bold" fontSize="xl">
          Register Device 3
        </Text>
  
        <Stack spacing={5} minW="full">
          <FormControl isRequired>
            <FormLabel>Device Name</FormLabel>
            <Input onChange={(e) => setDeviceName(e.target.value)} type="text" placeholder="John's Smart Fan" />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Brand</FormLabel>
            <Input isDisabled type="text" placeholder={deviceBrand} value={deviceBrand} />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Model</FormLabel>
            <Input isDisabled type="text" placeholder={deviceModel} value={deviceModel} />
          </FormControl>
          <FormControl isRequired>
            <FormLabel>Device Type</FormLabel>
            <Select onChange={(e) => setDeviceTypeName(e.target.value)} placeholder="Please a select a device type">
              {deviceTypeNames
                ? deviceTypeNames.map((item, i) => {
                    return (
                      <option key={i} value={item}>
                        {item}
                      </option>
                    );
                  })
                : null}
            </Select>
          </FormControl>
          <Stack direction="row" spacing={5} justifyContent="flex-end">
            <Button colorScheme="blue" onClick={onOpen}>
              Add Device Type
            </Button>
            <Button colorScheme="green" onClick={handleRegisterDevice}>
              Register Device 3
            </Button>
          </Stack>
        </Stack>
  
        <Modal blockScrollOnMount={false} isOpen={isOpen} onClose={onClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Add Device Type</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <FormControl isRequired>
                <FormLabel>Device Type</FormLabel>
                <Input onChange={(e) => setNewDeviceTypeName(e.target.value)} type="text" placeholder="Smart Fan" />
              </FormControl>
            </ModalBody>
            <ModalFooter>
              <Button variant="ghost" mr={3} onClick={onClose}>
                Close
              </Button>
              <Button colorScheme="green" onClick={handleAddNewDeviceType}>
                Add Device Type
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </Container>
      </Container>
    );
  }
  