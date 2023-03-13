import React from "react";
import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    Stack,
    Heading,
    Text,
    Button,
    Image,
    Popover,
    PopoverTrigger,
    PopoverContent,
    PopoverHeader,
    PopoverBody,
    PopoverFooter,
    PopoverArrow,
    PopoverCloseButton,
    PopoverAnchor,
} from "@chakra-ui/react";
import { Link, useNavigate } from "react-router-dom";
import { css } from "@emotion/react";


function SharedProfileCard({ ProfileName, Temp, Light, TV, ProfilePic }) {
    return (
        <Card
            direction={{ base: "column", sm: "row" }}
            overflow="hidden"
            variant="outline"
        >
            <Image
                objectFit="contain"
                maxW={{ base: "100%", sm: "200px" }}
                src={ProfilePic}
                alt="Profile Pic"
            />

            <Stack>
                <CardBody>
                    <Heading size="md">{ProfileName} Profile</Heading>

                    <Text>Temperature: {Temp}</Text>
                    <Text>Light: {Light}</Text>
                    <Text>TV: {TV}</Text>
                </CardBody>
                <CardFooter>
                    <Popover>
                        <PopoverTrigger>
                            <Button variant="solid" colorScheme="green">
                        More Details
                    </Button>
                        </PopoverTrigger>
                        <PopoverContent color='white' bg='blue.700' borderColor='blue.800'>
                            <PopoverArrow />
                            <PopoverCloseButton />
                            <PopoverHeader fontWeight="bold">Scenario Rules</PopoverHeader>
                            <PopoverBody>{Temp}<br/> {Light}<br/> {TV}</PopoverBody>
                        </PopoverContent>
                    </Popover>
                    
                    <Button marginLeft="10px" variant="solid" colorScheme="blue">
                        Use Profile
                    </Button>
                </CardFooter>
            </Stack>
        </Card>
    );
}

export function SharedProfileCardComponent({
    ProfileName,
    Temp,
    Light,
    TV,
    ProfilePic,
}) {
    return (
        <SharedProfileCard
            ProfileName={ProfileName}
            Temp={Temp}
            Light={Light}
            TV={TV}
            ProfilePic={ProfilePic}
        />
    );
}