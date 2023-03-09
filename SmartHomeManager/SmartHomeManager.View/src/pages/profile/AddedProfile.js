import { React, useState } from 'react';
import { CardComponent, } from "components/Profile/Card";
import { Grid, GridItem, Box } from "@chakra-ui/react"
import user1 from "./img/user1.png"
import user2 from "./img/user2.png"
import user3 from "./img/user3.png"
import user4 from "./img/user4.png"
import user5 from "./img/user5.png"
import user6 from "./img/user6.png"

export default function ProfileLanding() {
    return (
        <>
            <Grid templateColumns='repeat(3, 1fr)' gap={3} paddingTop="3em" paddingRight="3em" >
                <Box w="100%" m="0 auto" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent
                        imgSrc={user1}
                        profileName="Juleus"
                        description="Controlling television, fans, lights, air-conditioner in master bedroom"
                    >
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent
                        imgSrc={user2}
                        profileName="Bruce"
                        description="Controlling fans, lights, speakers in study room"
                    >
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent
                        imgSrc={user3}
                        profileName="Kang Chen"
                        description="Controlling fans, lights, ventilation system in kitchen"
                    >
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" mt="10em" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent
                        imgSrc={user4}
                        profileName="Chun boon"
                        description="Controlling fans, lights in bathroom"
                    >
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" mt="10em" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent
                        imgSrc={user5}
                        profileName="Liu Jun"
                        description="Controlling fans, lights, speakers, television in bedroom"
                    >
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" mt="10em" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent
                        imgSrc={user6}
                        profileName="Ryan"
                        description="Control fans"
                    >
                    </CardComponent>
                </Box>
            </Grid>
        </>


    )
}
