import { React } from 'react';
import { useNavigate, Link as RouterLink } from "react-router-dom";
import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    Stack,
    Button,
    Heading,
    Text,
    useColorModeValue,

} from '@chakra-ui/react';
import { ViewIcon, ViewOffIcon } from '@chakra-ui/icons';

export default function MyAccount() {

    const navigate = useNavigate()

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
                            <FormLabel>Username</FormLabel>
                            <Text>ThisIsUserName</Text>
                        </FormControl>

                        <FormControl id="email">
                            <FormLabel>Email address</FormLabel>
                            <Text> abc@gmail.com</Text>
                        </FormControl>
                        
                        <FormControl id="timezone">
                            <FormLabel>Timezone</FormLabel>
                            <Text>GMT +8</Text>
                        </FormControl>

                        <FormControl id="address">
                            <FormLabel>Address</FormLabel>
                            <Text>Dover St</Text>
                        </FormControl>

                        <Stack spacing={4}>
                            <Button
                                bg={'yellow.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'yellow.500',
                                }}>
                                Edit
                            </Button>
                            <Button
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Change Password
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