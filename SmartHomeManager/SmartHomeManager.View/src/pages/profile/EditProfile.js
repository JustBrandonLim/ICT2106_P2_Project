import { React, useState, } from 'react';
import { useNavigate, useLocation } from "react-router-dom"
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
import { ModalComponent } from "components/Profile/Modal";
import { SmallCloseIcon } from '@chakra-ui/icons';

function EditProfile() {
    const { state } = useLocation()
    const profileName = state?.profileName
    const description = state?.Description
    const imgSrc = state?.imgSrc

    console.log("imgSrc from state" + imgSrc)
    console.log("profileName from state" + profileName)
    console.log("description from state" + description)

    const [inputText, setInputText] = useState('');

    const handleInputChange = (event) => {
        setInputText(event.target.value);
    }

    const navigate = useNavigate();

    const handleSaveClick = (imgSrc, profileName, event) => {
        /*event.preventDefault();*/
        console.log("description from state" + event)
        navigate(`/profile-edited`, { state: { profileName, imgSrc, event } });
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
                <Heading lineHeight={1.1} fontSize={{ base: '2xl', sm: '3xl' }} >
                    Edit {profileName}
                </Heading>
                <FormControl id="userName">
                    <FormLabel >Profile Picture</FormLabel>
                    <Stack direction={['column', 'row']} spacing={2}>
                        <Center>
                            <Avatar size="xl" src={imgSrc} marginRight="15px" />
                        </Center>
                        <Center>
                            <ModalComponent />
                        </Center>
                    </Stack>
                </FormControl>
                <FormControl id="description" isRequired>
                    <FormLabel>Description</FormLabel>
                    <Input
                        placeholder={description}
                        _placeholder={{ color: 'gray.500', }}
                        value={inputText}
                        onChange={handleInputChange}
                        type="text"
                        
                        style={{ height: "150px", whiteSpace: "pre-wrap" }}
                    />
                </FormControl>
                <FormControl id="pin">
                    <FormLabel>Change Pin</FormLabel>
                    <Input
                        placeholder="4-digit pin"
                        _placeholder={{ color: 'gray.500', }}
                        maxLength={4}
                        type="text"
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
                        _hover={{
                            bg: 'blue.500',
                        }}
                        onClick={() => handleSaveClick(imgSrc, profileName, inputText)}                 >
                        Save
                    </Button>
                </Stack>
            </Stack>
        </Flex>
    );

}

export default function UserProfileEdit(): JSX.Element {
    return <EditProfile />
}