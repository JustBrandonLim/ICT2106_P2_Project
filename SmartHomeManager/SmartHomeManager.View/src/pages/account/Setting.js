import { React, useState } from 'react';
import { useNavigate, Link as RouterLink } from "react-router-dom";
import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    Stack,
    Button,
    Input,
    Heading,
    Text,
    useColorModeValue,

} from '@chakra-ui/react';
import { ViewIcon, ViewOffIcon } from '@chakra-ui/icons';

export default function MyAccount() {
    const accountId = localStorage.getItem('accountId');
    const [username, updateUsername] = useState("")
    const [email, updateEmail] = useState("")
    const [timezone, updateTimezone] = useState(0)
    const [address, updateAddress] = useState("")

    const navigate = useNavigate()

    //Retrieve account information
    const onLoad = () =>{
        fetch('https://localhost:7140/api/Accounts/'+accountId, {
            method: 'GET',
            headers: {
                accept: 'text/plain',
            },
        })
        .then(async response => {
            const msg = await response.json();
            if (response.ok) {
                updateUsername(msg["username"])
                updateEmail(msg["email"])
                updateTimezone(parseInt(msg["timezone"]))
                updateAddress(msg["address"])
            } 
        })
        .catch((err) => {
        });
    }
    onLoad()
    
    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>
                <Stack align={'center'}>
                    <Heading fontSize={'4xl'}>Update Account Details</Heading>
                </Stack>
                <Box
                    rounded={'lg'}
                    bg={useColorModeValue('white', 'gray.700')}
                    boxShadow={'lg'}
                    p={8}>
                    <Stack spacing={4}>
                        <FormControl id="username">
                            <FormLabel>Username:</FormLabel>
                            <Text>{username}</Text>
                        </FormControl>

                        <FormControl id="email">
                            <FormLabel>Email address:</FormLabel>
                            <Text>{email}</Text>
                        </FormControl>
                        
                        <FormControl id="timezone">
                            <FormLabel>Timezone:</FormLabel>
                            <Text>{ timezone>0 ? "GMT +"+timezone : "GMT "+timezone}</Text>
                        </FormControl>

                        <FormControl id="address">
                            <FormLabel>Address:</FormLabel>
                            <Text>{address}</Text>
                        </FormControl>

                        <Stack spacing={4}>
                            <Button
                                as={RouterLink}
                                to="/changepw"
                                bg={'yellow.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'yellow.500',
                                }}>
                                Change Password
                            </Button>
                            <Button
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Enable 2FA
                            </Button>
                            <Button
                                onClick={() => navigate("/two-factor-auth-setup", { replace: true })}
                                bg={'green.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'green.500',
                                }}>
                                Enable 2FA
                            </Button>
                        </Stack>
                    </Stack>
                </Box>
            </Stack>
        </Flex>
    );
}