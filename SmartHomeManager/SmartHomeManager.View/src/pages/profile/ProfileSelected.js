import { React, useState } from 'react';
import { useLocation } from "react-router-dom";
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
    Image,
    CardBody,
    CardFooter,
    Card,
    Text,
    Box,
    Grid
} from '@chakra-ui/react';
import { SmallCloseIcon } from '@chakra-ui/icons';
import user1 from "./img/user1.png"



export default function ProfileSelected(): JSX.Element {
    const [profileDetails, updateProfileDetails] = useState([])
    const location = useLocation();
    const profileId = location.state?.profileId;
    console.log(profileId)
    const getAllProfiles = async () => {
        await fetch(`https://localhost:7140/api/Profiles/${profileId}`, {
            method: 'GET',
            headers: {
                accept: 'text/plain'
            },
        })
            .then(async response => {
                const data = await response.json()

                if (response.ok) {
                    updateProfileDetails(data)
                }
            })
    }

    getAllProfiles()

    return (

        <Grid templateColumns='repeat(1, 1fr)' gap={3} padding="1.5em" maxWidth="500px">
                    <Box>
                        <Card
                            direction={{ base: 'column', sm: 'row' }}
                            overflow='hidden'
                            variant='outline'
                            size="md"
                            width="1015px"
                        >
                            <Image
                                objectFit='cover'
                                borderRadius='full'
                                boxSize='128px'
                                object-position="center"
                                marginTop="10px"
                                marginLeft="10px"
                                maxW={{ base: '80%', sm: '150px' }}
                                src={user1}
                                alt='img'
                            />
                            <Stack>
                                <CardBody >
                                    <Heading size='md'>{ profileDetails.name }</Heading>

                                    <Text py='2'>
                                {profileDetails.description }
                                    </Text>
                                </CardBody>

                                <CardFooter>
                                    <Button variant='solid' colorScheme='blue' marginRight="10px">
                                        Add Scenario
                                    </Button>
                                    <Button variant='solid' colorScheme='green' marginRight="10px">
                                        Share Profile
                                    </Button>
                                </CardFooter>
                            </Stack>
                        </Card>
                    </Box>            
            <Grid templateColumns='repeat(3, 1fr)' gap={3} paddingTop="3em" paddingRight="3em" width="990px">
                <Box width="330px">
                    <Card
                        direction={{ base: 'column', sm: 'row' }}
                        overflow='hidden'
                        variant='outline'
                        size="sm"
                    >
                        <Stack>
                            <CardBody>
                                <Heading size='md'>Device 1</Heading>

                                <Text py='2'>
                                    Xiao Mi Fan
                                </Text>
                            </CardBody>
                        </Stack>
                    </Card>
                </Box>
                <Box width="330px">
                    <Card
                        direction={{ base: 'column', sm: 'row' }}
                        overflow='hidden'
                        variant='outline'
                        size="sm"
                    >
                        <Stack>
                            <CardBody>
                                <Heading size='md'>Device 2</Heading>

                                <Text py='2'>
                                    Xiao Mi Aircon
                                </Text>
                            </CardBody>
                        </Stack>
                    </Card>
                </Box>
                <Box width="330px">
                    <Card
                        direction={{ base: 'column', sm: 'row' }}
                        overflow='hidden'
                        variant='outline'
                        size="sm"
                    >
                        <Stack>
                            <CardBody>
                                <Heading size='md'>Device 3</Heading>

                                <Text py='2'>
                                    Xiao Mi Television
                                </Text>
                            </CardBody>
                        </Stack>
                    </Card>
                </Box>
            </Grid>
            <Grid templateColumns='repeat(3, 1fr)' gap={3} paddingTop="3em" paddingRight="3em" width="990px">
                <Box width="330px">
                    <Card
                        direction={{ base: 'column', sm: 'row' }}
                        overflow='hidden'
                        variant='outline'
                        size="sm"
                    >
                        <Stack>
                            <CardBody>
                                <Heading size='md'>Scenario 1</Heading>

                                <Text py='2'>
                                    Night Settings for fans, lights
                                </Text>
                            </CardBody>

                            <CardFooter>
                                <Button variant='solid' colorScheme='blue' marginLeft="10px">
                                    Edit Scenario
                                </Button>
                                <Button variant='solid' colorScheme='red' marginLeft="10px">
                                    Delete Scenario
                                </Button>
                            </CardFooter>
                        </Stack>
                    </Card>
                </Box>
                <Box width="330px">
                    <Card
                        direction={{ base: 'column', sm: 'row' }}
                        overflow='hidden'
                        variant='outline'
                        size="sm"
                    >
                        <Stack>
                            <CardBody>
                                <Heading size='md'>Scenario 2</Heading>

                                <Text py='2'>
                                    Day settings for fans, lights, television
                                </Text>
                            </CardBody>

                            <CardFooter>
                                <Button variant='solid' colorScheme='blue' marginLeft="10px">
                                    Edit Scenario
                                </Button>
                                <Button variant='solid' colorScheme='red' marginLeft="10px">
                                    Delete Scenario
                                </Button>
                            </CardFooter>
                        </Stack>
                    </Card>
                </Box>
                <Box width="330px">
                    <Card
                        direction={{ base: 'column', sm: 'row' }}
                        overflow='hidden'
                        variant='outline'
                        size="sm"
                    >
                        <Stack>
                            <CardBody>
                                <Heading size='md'>Scenario 3</Heading>

                                <Text py='2'>
                                    Night settings for hot weather
                                </Text>
                            </CardBody>

                            <CardFooter>
                                <Button variant='solid' colorScheme='blue' marginLeft="10px">
                                    Edit Scenario
                                </Button>
                                <Button variant='solid' colorScheme='red' marginLeft="10px">
                                    Delete Scenario
                                </Button>
                            </CardFooter>
                        </Stack>
                    </Card>
                </Box>
            </Grid>
        </Grid>
    );
}