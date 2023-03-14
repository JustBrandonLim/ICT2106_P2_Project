/* eslint-disable react/prop-types */
import { React, useState } from 'react'
import {
	Flex,
	Box,
	Image,
	useColorModeValue,
	Button,
	Spacer,
} from '@chakra-ui/react'

function PriceButton({ price }) {
	const [isHovered, setIsHovered] = useState(false)

	const handleMouseEnter = () => {
		setIsHovered(true)
	}

	const handleMouseLeave = () => {
		setIsHovered(false)
	}

	return (
		<Button
			colorScheme="blue"
			width={'full'}
			transition="all 0.3s"
			bg={isHovered ? 'blue.500' : 'blue.700'}
			color={isHovered ? 'white' : 'blue.300'}
			onMouseEnter={handleMouseEnter}
			onMouseLeave={handleMouseLeave}
		>
			{isHovered ? 'Buy now!' : `$ ${price.toFixed(2)}`}
		</Button>
	)
}

function StoreCard({ imageURL, name, price }) {
	return (
		<Flex w="full" alignItems="center" justifyContent="center" h="auto">
			<Flex
				bg={useColorModeValue('white', 'gray.800')}
				maxW="sm"
				h="320px"
				borderWidth="1px"
				direction={'column'}
				rounded="lg"
				shadow="lg"
			>
				<Image
					src={imageURL}
					alt={`Picture of ${name}`}
					roundedTop="lg"
					w="full"
					h="160px"
					objectFit="cover"
				/>

				<Flex direction={{ base: 'row', md: 'column' }} p="4" h={'full'}>
					<Flex mt="1" justifyContent="space-between" alignContent="center">
						<Box fontSize="lg" fontWeight="semibold">
							{name}
						</Box>
					</Flex>
					<Spacer />
					<Flex justifyContent="space-between" alignContent="center">
						<PriceButton price={price} />
					</Flex>
				</Flex>
			</Flex>
		</Flex>
	)
}

export default StoreCard
