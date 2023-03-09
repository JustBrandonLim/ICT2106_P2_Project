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
} from '@chakra-ui/react';
import user6 from "./img/user6.png"
import { ModalComponent } from '../../components/Profile/Modal'
import { Switch } from '@chakra-ui/react'

function AddedProfilePage() {

    const [inputUserName, setInputUserName] = useState('');
    const [inputDescription, setInputDescription] = useState('');
    const [inputPin, setInputPin] = useState('');
    const navigate = useNavigate();


    const handleUserNameChange = (event) => {
        setInputUserName(event.target.value)
    }

    const handleDescriptionChange = (event) => {
        setInputDescription(event.target.value)
    }

    const handlePinChange = (event) => {
        setInputPin(event.target.value)
    }

    const handleSubmitClick = () => {
        navigate(`/profile-added`);
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
                <Heading lineHeight={1.1} fontSize={{ base: '2xl', sm: '3xl' }}>
                    Add Profile
                </Heading>
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
                        type="text"
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
    return ( <AddedProfilePage/>)
}