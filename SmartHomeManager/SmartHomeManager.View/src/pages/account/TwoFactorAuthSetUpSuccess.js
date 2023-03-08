import { React } from 'react';
import { useNavigate, Link as RouterLink } from "react-router-dom";
import {
    Flex,
    Box,
    Stack,
    Button,
    Heading,
    useColorModeValue,
} from '@chakra-ui/react';

export default function TwoFactorAuthSetUpSuccess() {
    const navigate = useNavigate()
    
    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>

                <Box
                    rounded={'lg'}
                    bg={useColorModeValue('white', 'gray.700')}
                    boxShadow={'lg'}
                    p={8}>
                        <Stack spacing={4}>
                            <Stack align={'center'}>
                                <Heading textAlign={'center'} fontSize={'4xl'}>
                                    Your two-factor authentication has been set up successfully.
                                </Heading>
                            </Stack>
                            <Button
                                as={RouterLink}
                                to="/myaccount"
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Settings
                            </Button>
                            <Button
                                as={RouterLink}
                                to="/"
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Back to Home
                            </Button>
                        </Stack>
                </Box>
            </Stack>
        </Flex>
    );
}
