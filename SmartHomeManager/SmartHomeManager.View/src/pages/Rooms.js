import { CloseIcon, EditIcon } from '@chakra-ui/icons'
import {
	Box,
	Button,
	Card,
	CardBody,
	CardHeader,
	Heading,
	HStack,
	IconButton,
	Input,
	Modal,
	ModalBody,
	ModalCloseButton,
	ModalContent,
	ModalFooter,
	ModalHeader,
	ModalOverlay,
	SimpleGrid,
	useDisclosure,
} from '@chakra-ui/react'
import React, { useState, useEffect } from 'react'

export default function Rooms() {
	const [rooms, setRooms] = useState([])
	const { isOpen, onOpen, onClose } = useDisclosure()
	const [selectedRoom, setSelectedRoom] = useState({})
	const [newName, setNewName] = useState('Room')
	const [newRoomName, setNewRoomName] = useState('Room')

	useEffect(() => {
		const fetchData = async () => {
			try {
				const res = await fetch('https://localhost:7140/api/Rooms', {
					method: 'GET',
					headers: {
						accept: 'text/plain',
					},
				})
				const data = await res.json()
				setRooms(data)
			} catch (error) {
				console.error(error)
			}
		}
		fetchData()
	}, [])

	const handleEdit = async (id, newName) => {
		try {
			const res = await fetch(`https://localhost:7140/api/Rooms/${id}`, {
				method: 'PUT',
				headers: {
					accept: 'text/plain',
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({ name: newName }),
			})
			if (res.ok) {
				const updatedRooms = rooms.map((room) => {
					if (room.roomId === id) {
						return { ...room, name: newName }
					}
					return room
				})
				setRooms(updatedRooms)
			} else {
				console.error(res.statusText)
			}
		} catch (error) {
			console.error(error)
		}
		onClose()
	}

	const handleDelete = async (id) => {
		try {
			const res = await fetch(`https://localhost:7140/api/Rooms/${id}`, {
				method: 'DELETE',
				headers: {
					accept: 'text/plain',
				},
			})
			if (res.ok) {
				const updatedRooms = rooms.filter((room) => room.roomId !== id)
				setRooms(updatedRooms)
			} else {
				console.error(res.statusText)
			}
		} catch (error) {
			console.error(error)
		}
	}

	const handleAdd = async (newRoomName) => {
		try {
			const res = await fetch(`https://localhost:7140/api/Rooms`, {
				method: 'POST',
				headers: {
					accept: 'text/plain',
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({
					name: newRoomName,
					accountId: 'C03F4271-BAF9-4E08-8853-F5B3291676B7',
				}),
			})
			if (res.ok) {
				const newRoom = await res.json()
				setRooms([...rooms, newRoom])
			} else {
				console.error(res.statusText)
			}
		} catch (error) {
			console.error(error)
		}
	}

	const handleOpen = (room) => {
		setSelectedRoom(room)
		onOpen()
	}

	const handleInputChange = (event) => {
		setNewName(event.target.value)
		setSelectedRoom({ ...selectedRoom, name: event.target.value })
	}

	return (
		<>
			<Modal isOpen={isOpen} onClose={onClose}>
				<ModalOverlay />
				<ModalContent>
					<ModalHeader>Edit Room Name</ModalHeader>
					<ModalCloseButton />
					<ModalBody>
						<Input
							placeholder={selectedRoom.name}
							value={selectedRoom.name}
							onChange={handleInputChange}
						/>
					</ModalBody>
					<ModalFooter>
						<Button
							colorScheme="blue"
							mr={3}
							variant="outline"
							onClick={onClose}
						>
							Close
						</Button>
						<Button
							colorScheme={'green'}
							onClick={() => {
								handleEdit(selectedRoom.roomId, newName)
							}}
						>
							Confirm
						</Button>
					</ModalFooter>
				</ModalContent>
			</Modal>
			{rooms.map((room) => (
				<Box key={room.roomId} mb="8">
					<HStack mb="2" spacing={'1'}>
						<Heading size={'md'}>{room.name}</Heading>
						<IconButton
							variant={'ghost'}
							icon={<EditIcon />}
							size={'sm'}
							onClick={() => handleOpen(room)}
						/>
						<IconButton
							variant={'outline'}
							icon={<CloseIcon />}
							size={'sm'}
							colorScheme={'red'}
							onClick={() => handleDelete(room.roomId)}
						/>
					</HStack>
					<SimpleGrid
						spacing={4}
						templateColumns="repeat(6, minmax(200px, 1fr))"
					>
						<Card key="1">
							<CardHeader>
								<Heading size="md">Light</Heading>
							</CardHeader>
							<CardBody>Device 1</CardBody>
						</Card>
						<Card key="2">
							<CardHeader>
								<Heading size="md">Fan</Heading>
							</CardHeader>
							<CardBody>Device 2</CardBody>
						</Card>
						<Card key="3">
							<CardHeader>
								<Heading size="md">TV</Heading>
							</CardHeader>
							<CardBody>Device 3</CardBody>
						</Card>
					</SimpleGrid>
				</Box>
			))}

			<Card>
				<CardHeader>
					<Heading size="md">New room?</Heading>
				</CardHeader>
				<CardBody>
					<Input
						value={newRoomName}
						onChange={(event) => {
							setNewRoomName(event.target.value)
						}}
					/>
				</CardBody>
				<Button onClick={() => handleAdd(newRoomName)}>Add Room</Button>
			</Card>
		</>
	)
}
