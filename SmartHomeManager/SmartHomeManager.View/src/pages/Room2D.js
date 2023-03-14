import { React, useState } from 'react'
import { AddIcon, MinusIcon, PlusSquareIcon } from '@chakra-ui/icons'
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
	Stack,
	useColorModeValue,
} from '@chakra-ui/react'
import GridLayout from 'react-grid-layout'
import { MdPowerSettingsNew } from 'react-icons/md'

export default function Room2D() {
	const [layout, setLayout] = useState([
		{ i: 'a', x: 0, y: 0, w: 2, h: 2 },
		{ i: 'b', x: 2, y: 0, w: 3, h: 2, minW: 2, maxW: 4 },
		{ i: 'c', x: 5, y: 0, w: 2, h: 2 },
	])
	const [rooms, setRooms] = useState(['Bedroom', 'Toilet', 'Kitchen'])
	const [devices, setDevices] = useState(['Light', 'Fan'])
	const [roomName, setRoomName] = useState('')
	const [isDialogOpen, setIsDialogOpen] = useState(false)
	const [selectedRoom, setSelectedRoom] = useState('')
	const [devicesState, setDevicesState] = useState({
		'device-1': { name: 'Light', state: true, isLoading: false },
		'device-2': { name: 'Fan', state: true, isLoading: false },
	})

	const handleAddRoom = () => {
		setIsDialogOpen(true)
	}

	const handleRoomNameChange = (event) => {
		setRoomName(event.target.value)
	}

	const handleDialogClose = () => {
		setIsDialogOpen(false)
		setRoomName('')
	}

	const onAddRoom = (roomName) => {
		const newRoom = {
			i: `room-${rooms.length + 1}`,
			x: 0,
			y: Infinity,
			w: 2,
			h: 2,
			add: true,
			name: roomName,
		}
		setLayout([...layout, newRoom])
	}

	const handleDialogSubmit = () => {
		const newRoom = roomName
		setRooms([...rooms, newRoom])
		onAddRoom(newRoom)
		setIsDialogOpen(false)
		setRoomName('')
	}
	const removeRoom = (roomName) => {
		const roomIndex = rooms.indexOf(roomName)
		if (roomIndex === -1) {
			return
		}

		setSelectedRoom('')
		const newRooms = [...rooms]
		newRooms.splice(roomIndex, 1)
		setRooms(newRooms)

		const newLayout = [...layout]
		const roomToRemove = newLayout[roomIndex]
		newLayout.splice(roomIndex, 1)
		setLayout(newLayout)

		// shift the layout items above the removed item up by one row
		newLayout.forEach((room) => {
			if (room.y > roomToRemove.y) {
				room.y -= roomToRemove.h
			}
		})
	}

	const handleDeviceClick = (deviceName) => {
		setDevicesState((prevState) => {
			const newState = { ...prevState }
			newState[deviceName] = !newState[deviceName]
			return newState
		})
	}

	return (
		<>
			<Flex verticalAlign={'center'} mb="2" gap={'2'}>
				<Heading>2D Room View</Heading>
				<Button
					leftIcon={<AddIcon />}
					onClick={() => {
						handleAddRoom()
					}}
				>
					Add Room
				</Button>
				<Button
					ms="2"
					leftIcon={<MinusIcon />}
					colorScheme={selectedRoom ? 'red' : 'gray'}
					onClick={() => {
						removeRoom(selectedRoom)
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
						borderRadius={'md'}
						direction={'column'}
					>
						<Heading color={'gray.200'} size={'sm'}>
							{room}
						</Heading>

						<Stack spacing={4} direction="row" align="center" py="2">
							{devices.map((device) => (
								<Button
									key={`${room}-${device}`}
									colorScheme={
										devicesState[`${room}-${device}`] ? 'gray' : 'teal'
									}
									size="xs"
									leftIcon={<MdPowerSettingsNew />}
									onClick={() => {
										handleDeviceClick(`${room}-${device}`)
									}}
								>
									{device}
								</Button>
							))}
						</Stack>
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
						<Button colorScheme={'green'} onClick={handleDialogSubmit}>
							Add Room
						</Button>
					</ModalFooter>
				</ModalContent>
			</Modal>
		</>
	)
}
