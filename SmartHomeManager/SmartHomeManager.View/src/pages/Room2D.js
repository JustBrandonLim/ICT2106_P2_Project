import { React, useState } from "react";
import { AddIcon, MinusIcon, PlusSquareIcon } from "@chakra-ui/icons";
import {
  Button,
  Center,
  Container,
  Flex,
  Heading,
  IconButton,
  Input,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  Select,
  useColorModeValue,
} from "@chakra-ui/react";
import GridLayout from "react-grid-layout";
import { color } from "Faker/lib/internet";

export default function Room2D() {
  const [layout, setLayout] = useState([
    { i: "a", x: 0, y: 0, w: 1, h: 2 },
    { i: "b", x: 1, y: 0, w: 3, h: 2, minW: 2, maxW: 4 },
    { i: "c", x: 4, y: 0, w: 1, h: 2 },
  ]);
  const [rooms, setRooms] = useState(["Bedroom", "Toilet", "Kitchen"]);
  const [roomName, setRoomName] = useState("");
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [selectedRoom, setSelectedRoom] = useState("");

  const handleAddRoom = () => {
    setIsDialogOpen(true);
  };

  const handleRoomNameChange = (event) => {
    setRoomName(event.target.value);
  };

  const handleDialogClose = () => {
    setIsDialogOpen(false);
    setRoomName("");
  };

  const onAddItem = (roomName) => {
    const newItem = {
      i: `room-${rooms.length + 1}`,
      x: 0,
      y: Infinity,
      w: 2,
      h: 2,
      add: true,
      name: roomName,
    };
    setLayout([...layout, newItem]);
  };

  const handleDialogSubmit = () => {
    const newRoom = roomName;
    setRooms([...rooms, newRoom]);
    onAddItem(newRoom);
    setIsDialogOpen(false);
    setRoomName("");
  };
  const removeRoom = (roomName) => {
    const roomIndex = rooms.indexOf(roomName);
    if (roomIndex === -1) {
      return;
    }

    setSelectedRoom("");
    const newRooms = [...rooms];
    newRooms.splice(roomIndex, 1);
    setRooms(newRooms);

    const newLayout = [...layout];
    const itemToRemove = newLayout[roomIndex];
    newLayout.splice(roomIndex, 1);
    setLayout(newLayout);

    // shift the layout items above the removed item up by one row
    newLayout.forEach((item) => {
      if (item.y > itemToRemove.y) {
        item.y -= itemToRemove.h;
      }
    });
  };

  return (
    <>
      <Flex verticalAlign={"center"} mb="2" gap={"2"}>
        <Heading>2D Room View</Heading>
        <Button
          leftIcon={<AddIcon />}
          onClick={() => {
            handleAddRoom();
          }}
        >
          Add Room
        </Button>
        <Button
          ms="2"
          leftIcon={<MinusIcon />}
          colorScheme={selectedRoom ? "red" : "gray"}
          onClick={() => {
            removeRoom(selectedRoom);
          }}
        >
          Remove Room
        </Button>
        <Select
          placeholder="Select room to delete"
          w="auto"
          value={selectedRoom}
          onChange={(e) => setSelectedRoom(e.target.value)}
        >
          {rooms.map((room, index) => (
            <option key={index}>{room}</option>
          ))}
        </Select>
      </Flex>
      <GridLayout
        className="layout"
        layout={layout}
        cols={8}
        rowHeight={40}
        width={1200}
        onLayoutChange={(newLayout) => setLayout(newLayout)}
      >
        {rooms.map((room, index) => (
          <Flex
            key={layout[index].i}
            bg="gray.700"
            h="full"
            p="4"
            borderRadius={"md"}
          >
            <Heading color={"gray.200"} size={"sm"}>
              {room}
            </Heading>
          </Flex>
        ))}
      </GridLayout>
      <Modal isOpen={isDialogOpen} onClose={handleDialogClose}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Add Room</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <Input
              placeholder="Enter room name"
              value={roomName}
              onChange={handleRoomNameChange}
            />
          </ModalBody>
          <ModalFooter>
            <Button colorScheme={"green"} onClick={handleDialogSubmit}>
              Add Room
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
}
