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

    const [profileDetails, updateProfileDetails] = useState([])
    const getAllProfiles = async () => {
        const accountId = "11111111-1111-1111-1111-111111111111";
        await fetch(`https://localhost:7140/api/Profiles/get-profiles/${accountId}`, {
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
        <>
            <Grid templateColumns='repeat(3, 1fr)' templateRows="repeat(2, 300px)" gap={3} paddingTop="3em" paddingRight="3em" >

                {
                    profileDetails.map((item) => (
                            <Box key={item.profileId}  w="100%" padding="10dp" marginBottom="10dp" maxW="400px" h="100px" bg="gray.50">
                                <CardComponent
                                    imgSrc={user1}
                                    profileName={item.name}
                                    description={item.description}
                                >
                                </CardComponent>
                            </Box>

                    ))
                }
                    <Box w="100%" m="0 auto" maxW="400px" h="100px" bg="gray.50">
                        <CardComponent
                            imgSrc={user6}
                            profileName="Add Profile"
                            description="Click on Add Profile Button"
                        >
                        </CardComponent>
                    </Box>

            </Grid>
        </>


    )
}
