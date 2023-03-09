import { React, useState } from "react";
import { Card, CardHeader, CardBody, CardFooter, Stack, Heading, Text, Button, Image } from '@chakra-ui/react'
import { Link, useNavigate } from "react-router-dom"
import { css } from '@emotion/react';

function ProfileCard({ profileName, imgSrc, Description }) {

    const navigate = useNavigate();

    const cardHoverStyle = css`&:hover {
    cursor: pointer;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    transform: translateY(-2px);
    }`;


    const handleEditPinClick = () => {
        //Handle edit pin click
    }
    const handleEditProfileClick = (profileName, imgSrc, Description) => {
        // Handle edit profile click
        navigate(`/edit-profile`, { state: { profileName, imgSrc, Description } });
    }

    const handleAddProfileClick = () => {
        // Handle add profile click
        navigate(`/add-profile`)
    }
    return (
        <Link to="/profile-juleus">

            <Card
                css={cardHoverStyle }
                direction={{ base: 'column', sm: 'row' }}
                size="sm"
                h="250px"
                overflow='hidden'>

                <Image
                    objectFit='cover'
                    borderRadius='full'
                    boxSize='128px'
                    object-position="center"
                    marginTop="10px"
                    marginLeft="10px"
                    maxW={{ base: '80%', sm: '150px' }}
                    src={imgSrc}
                    alt='img'
                />
                <Stack>
                    <CardBody>
                        <Heading size='md' margin="10px">{profileName}</Heading>

                        <Text py='2' marginRight="10px" marginLeft="10px">
                            {Description}
                        </Text>
                    </CardBody>
                    {profileName !== "Add Profile" ? (
                        <CardFooter>
                            <Button
                                variant='solid'
                                colorScheme='blue'
                                onClick={() => handleEditProfileClick(profileName, imgSrc, Description)}
                                marginLeft="10px">
                                Edit Profile
                            </Button>
                        </CardFooter>
                    ) : (
                        <CardFooter>
                            <Button variant='solid' colorScheme='blue' onClick={handleAddProfileClick} margin="10px">
                                Add Profile
                            </Button>
                        </CardFooter>
                    )}
                </Stack>
            </Card>
        </Link>
    )
}

export function CardComponent({ imgSrc, profileName, description }) {
    return <ProfileCard profileName={profileName} imgSrc={imgSrc} Description={description} />
}