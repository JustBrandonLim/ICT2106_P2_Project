import { React, useState } from 'react';
import { useNavigate } from "react-router-dom"
import {
    Button,
    Flex,
    FormControl,
    FormLabel,
    Heading,
    Input,
    Stack,
    useColorModeValue,
    HStack,
    Avatar,
    AvatarBadge,
    IconButton,
    Center,
    CloseButton,
} from '@chakra-ui/react';
import user6 from "./img/user6.png"
import { ModalComponent } from '../../components/Profile/Modal'
import { Switch } from '@chakra-ui/react'

function AddedProfilePage() {

    const [inputUserName, setInputUserName] = useState('');
    const [inputDescription, setInputDescription] = useState('');
    const [inputPin, setInputPin] = useState('');
    const navigate = useNavigate();

    //For validation
    const [profileCreateFailStatus, updateProfileCreateFailStatus] = useState(false)
    const [errorMessage, updateErrorMessage] = useState("")
    const [profileCreationMessage, updateProfileCreationMessage] = useState("")


    const handleUserNameChange = (event) => {
        setInputUserName(event.target.value)
    }

    const handleDescriptionChange = (event) => {
        setInputDescription(event.target.value)
    }

    const handlePinChange = (event) => {
        const pin = event.target.value.trim().slice(0, 4); // Trims whitespace and limits input to 4 characters
        setInputPin(pin || null);
    }
    //Access API to create profile
    const handleSubmitClick = () => {
        //JSO stringify to send to api controller
        const accountId = "11111111-1111-1111-1111-111111111111";
        if (inputPin == "") {
            setInputPin(null)
        }
        const addProfileObj = {
            "Name": inputUserName, "Description": inputDescription, "Pin": inputPin, "AccountId": accountId
        }

        fetch('https://localhost:7140/api/Profiles', {
            method: 'POST',
            body: JSON.stringify(addProfileObj),
            headers: {
                'Content-type': 'application/problem+json; charset=utf-8',
            },
        })
            .then(async response => {
                if (response.ok) {
                    /*Ok(1) - Profile Created*/
                    updateProfileCreateFailStatus(false);
                    navigate("/profiles", { replace: true });
                }
                else {
                    /*BadRequest(1) - Profile Not Created*/
                    updateErrorMessage("Profile not created.")
                }
                updateProfileCreationMessage("Your account fail to create: " + errorMessage + " Please try again.")
                updateProfileCreateFailStatus(true);
            })
    }
    //Navigate to profiles page when closing the card
    const handleCloseClick = () => {
        //Navigate back to profile
        navigate(`/profiles`);
    }

    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack
                spacing={4}
                w={'full'}
                maxW={'md'}
                bg={useColorModeValue('white', 'gray.700')}
                rounded={'xl'}
                boxShadow={'lg'}
                p={6}
                my={12}>
                <Stack direction='row'>
                    <Flex justify="space-between" align="center" w="100%">
                        <Heading lineHeight={1.1} fontSize={{ base: '2xl', sm: '3xl' }}>
                            Add Profile
                        </Heading>
                        <CloseButton onClick={handleCloseClick} />
                    </Flex>
                </Stack>
                
                <FormControl id="userName">
                    <FormLabel>Profile Picture</FormLabel>
                    <Stack direction={['column', 'row']} spacing={6}>
                        <Center>
                            <Avatar size="xl" src={user6}>
                            </Avatar>
                        </Center>
                        <Center>
                            <ModalComponent />
                        </Center>
                    </Stack>
                </FormControl>
                <FormControl id="userName" isRequired>
                    <FormLabel>Name</FormLabel>
                    <Input
                        placeholder="UserName"
                        _placeholder={{ color: 'gray.500' }}
                        type="text"
                        value={inputUserName}
                        onChange={handleUserNameChange}
                    />
                </FormControl>
                <FormControl id="description" isRequired>
                    <FormLabel>Description</FormLabel>
                    <Input
                        placeholder="Enter profile description"
                        _placeholder={{ color: 'gray.500' }}
                        type="text"
                        style={{ height: "150px", whiteSpace: "pre-wrap" }}
                        value={inputDescription}
                        onChange={handleDescriptionChange}
                    />
                </FormControl>
                <FormControl id="userPin">
                    <FormLabel>Set Pin</FormLabel>
                    <Input
                        placeholder="set pin if you want parental controls"
                        _placeholder={{ color: 'gray.500' }}
                        type="number"
                        maxLength={4}
                        value={inputPin}
                        onChange={handlePinChange}
                    />
                </FormControl>


                <Stack spacing={6} direction={['column', 'row']}>
                    <Button
                        bg={'red.400'}
                        color={'white'}
                        w="full"
                        _hover={{
                            bg: 'red.500',
                        }}>
                        Cancel
                    </Button>
                    <Button
                        bg={'blue.400'}
                        color={'white'}
                        w="full"
                        onClick={handleSubmitClick}
                        _hover={{
                            bg: 'blue.500',
                        }}>
                        Submit
                    </Button>
                </Stack>
            </Stack>
        </Flex>
    )
}

export default function AddedProfile(): JSX.Element {
    return (<AddedProfilePage />)
}