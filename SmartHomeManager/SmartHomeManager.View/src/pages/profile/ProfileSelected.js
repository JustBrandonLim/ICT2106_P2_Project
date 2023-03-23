import { React, useState, useRef } from "react";
import { useLocation, useNavigate } from "react-router-dom";
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
  Grid,
  AlertDialog,
  AlertDialogBody,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogContent,
  AlertDialogOverlay,
  useDisclosure,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
} from "@chakra-ui/react";
import { SmallCloseIcon } from "@chakra-ui/icons";
import user1 from "./img/user1.png";

export default function ProfileSelected(): JSX.Element {
  const [profileDetails, updateProfileDetails] = useState([]);
  const location = useLocation();
  const profileId = location.state?.profileId;


  const navigate = useNavigate();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = useRef();
  const pinCheckRef = useRef();
  const [inputPin, setInputPin] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);


  // Function to retrieve all the profile
  const getAllProfiles = async () => {
    await fetch(`https://localhost:7140/api/Profiles/${profileId}`, {
      method: "GET",
      headers: {
        accept: "text/plain",
      },
    }).then(async (response) => {
      const data = await response.json();

      if (response.ok) {
        updateProfileDetails(data);
      }
    });
  };

 // Function to check if it's adult profile
  const checkAdultProfile = async () =>{
    console.log("running check adult profile function ")
    const idObj = {"ProfileId": profileId}
    await fetch('https://localhost:7140/api/Profiles/adult-checker',{
      method: "POST",
      body: JSON.stringify(idObj),
      headers:{
        "Content-Type": "application/problem+json; charset=utf-8 ",
      },
    }).then(async (response) => {

      if (response.ok){
        const result = await response.json()
        if (result == 1){
          // adult profile\
          console.log("adult");
          navigate("/selectnearbydevice");

        }
        else{
          // child profile
          console.log("child");
          onOpen();

        }
      }
    }) ;
  };

  getAllProfiles();


  const handlePinChange = (event) => {
    const pin = event.target.value.trim().slice(0, 4); // Trims whitespace and limits input to 4 characters
    setInputPin(pin || null);
  };


  // modals
  function handleOpenModal() {
    setIsModalOpen(true);
  }

  function handleCloseModal() {
    setIsModalOpen(false);
  }

  // Function to verify pin details
  const pinObject = { "Pin": inputPin, "ProfileId": profileId };
  const handleSubmitClick = async () => {
    console.log(pinObject);
    await fetch(`https://localhost:7140/api/Profiles/check-Pin`, {
      method: "POST",
      body: JSON.stringify(pinObject),
      headers: {
        "Content-Type": "application/problem+json; charset=utf-8 ",
      },
    }).then(async (response) => {
      if (response.ok) {
        // find the result of the response 
        var result = await response.json();
        console.log("the result is " + result)
        if (result == 1) {
          // child profile with correct pin
          navigate("/selectnearbydevice");
        } else if (result == 2) {
          {
            handleOpenModal();
          } // child profile with wrong pin
        }      
      }else{
        // do nothing for now
      }
    });
  };

  return (
    <>
      <Grid
        templateColumns="repeat(1, 1fr)"
        gap={3}
        padding="1.5em"
        maxWidth="500px"
      >
        <Box>
          <Card
            direction={{ base: "column", sm: "row" }}
            overflow="hidden"
            variant="outline"
            size="md"
            width="1015px"
          >
            <Image
              objectFit="cover"
              borderRadius="full"
              boxSize="128px"
              object-position="center"
              marginTop="10px"
              marginLeft="10px"
              maxW={{ base: "80%", sm: "150px" }}
              src={user1}
              alt="img"
            />
            <Stack>
              <CardBody>
                <Heading size="md">{profileDetails.name}</Heading>

                <Text py="2">{profileDetails.description}</Text>
              </CardBody>

              <CardFooter>
                <Button variant="solid" colorScheme="blue" marginRight="10px">
                  Add Scenario
                </Button>
                <Button variant="solid" colorScheme="green" marginRight="10px">
                  Share Profile
                </Button>
                <Button colorScheme="red" onClick={checkAdultProfile}>
                  Add devices
                </Button>

                <AlertDialog
                  isOpen={isOpen}
                  leastDestructiveRef={cancelRef}
                  onClose={onClose}
                  isCentered
                >
                  <AlertDialogOverlay>
                    <AlertDialogContent>
                      <AlertDialogHeader fontSize="lg" fontWeight="bold">
                        Attention!
                      </AlertDialogHeader>
                      <AlertDialogBody>
                        <FormControl>
                          Please enter your 4 digit PIN:
                          <Input
                            variant={"outline"}
                            type="number"
                            maxLength={4}
                            placeholder="PIN"
                            value={inputPin}
                            onChange={(event) =>
                              setInputPin(event.target.value)
                            }
                          />
                        </FormControl>
                      </AlertDialogBody>
                      <AlertDialogFooter>
                        <Button ref={pinCheckRef} onClick={handleSubmitClick}>
                          Submit
                        </Button>

                        <Modal isOpen={isModalOpen} onClose={handleCloseModal} isCentered>
                        <ModalOverlay />
                        <ModalContent>
                            <ModalHeader fontSize="lg" fontWeight="bold"
                            >Error!</ModalHeader>
                            <ModalCloseButton />
                            <ModalBody fontSize="lg" >
                                You have keyed in the wrong PIN,
                                please try again. 
                            </ModalBody>

                            <ModalFooter>
                                <Button colorScheme='red' mr={3} onClick={handleCloseModal}>
                                    Ok
                                </Button>
                            </ModalFooter>
                        </ModalContent>
                    </Modal>

                        <Button
                          ref={cancelRef}
                          colorScheme="red"
                          onClick={onClose}
                          ml={3}
                        >
                          Cancel
                        </Button>
                      </AlertDialogFooter>
                    </AlertDialogContent>
                  </AlertDialogOverlay>
                </AlertDialog>
              </CardFooter>
            </Stack>
          </Card>
        </Box>
        <Grid
          templateColumns="repeat(3, 1fr)"
          gap={3}
          paddingTop="3em"
          paddingRight="3em"
          width="990px"
        >
          <Box width="330px">
            <Card
              direction={{ base: "column", sm: "row" }}
              overflow="hidden"
              variant="outline"
              size="sm"
            >
              <Stack>
                <CardBody>
                  <Heading size="md">Device 1</Heading>

                  <Text py="2">Xiao Mi Fan</Text>
                </CardBody>
              </Stack>
            </Card>
          </Box>
          <Box width="330px">
            <Card
              direction={{ base: "column", sm: "row" }}
              overflow="hidden"
              variant="outline"
              size="sm"
            >
              <Stack>
                <CardBody>
                  <Heading size="md">Device 2</Heading>

                  <Text py="2">Xiao Mi Aircon</Text>
                </CardBody>
              </Stack>
            </Card>
          </Box>
          <Box width="330px">
            <Card
              direction={{ base: "column", sm: "row" }}
              overflow="hidden"
              variant="outline"
              size="sm"
            >
              <Stack>
                <CardBody>
                  <Heading size="md">Device 3</Heading>

                  <Text py="2">Xiao Mi Television</Text>
                </CardBody>
              </Stack>
            </Card>
          </Box>
        </Grid>
        <Grid
          templateColumns="repeat(3, 1fr)"
          gap={3}
          paddingTop="3em"
          paddingRight="3em"
          width="990px"
        >
          <Box width="330px">
            <Card
              direction={{ base: "column", sm: "row" }}
              overflow="hidden"
              variant="outline"
              size="sm"
            >
              <Stack>
                <CardBody>
                  <Heading size="md">Scenario 1</Heading>

                  <Text py="2">Night Settings for fans, lights</Text>
                </CardBody>

                <CardFooter>
                  <Button variant="solid" colorScheme="blue" marginLeft="10px">
                    Edit Scenario
                  </Button>
                  <Button variant="solid" colorScheme="red" marginLeft="10px">
                    Delete Scenario
                  </Button>
                </CardFooter>
              </Stack>
            </Card>
          </Box>
          <Box width="330px">
            <Card
              direction={{ base: "column", sm: "row" }}
              overflow="hidden"
              variant="outline"
              size="sm"
            >
              <Stack>
                <CardBody>
                  <Heading size="md">Scenario 2</Heading>

                  <Text py="2">Day settings for fans, lights, television</Text>
                </CardBody>

                <CardFooter>
                  <Button variant="solid" colorScheme="blue" marginLeft="10px">
                    Edit Scenario
                  </Button>
                  <Button variant="solid" colorScheme="red" marginLeft="10px">
                    Delete Scenario
                  </Button>
                </CardFooter>
              </Stack>
            </Card>
          </Box>
          <Box width="330px">
            <Card
              direction={{ base: "column", sm: "row" }}
              overflow="hidden"
              variant="outline"
              size="sm"
            >
              <Stack>
                <CardBody>
                  <Heading size="md">Scenario 3</Heading>

                  <Text py="2">Night settings for hot weather</Text>
                </CardBody>

                <CardFooter>
                  <Button variant="solid" colorScheme="blue" marginLeft="10px">
                    Edit Scenario
                  </Button>
                  <Button variant="solid" colorScheme="red" marginLeft="10px">
                    Delete Scenario
                  </Button>
                </CardFooter>
              </Stack>
            </Card>
          </Box>
        </Grid>
      </Grid>
    </>
  );
}
