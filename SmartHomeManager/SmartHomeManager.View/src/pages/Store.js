import { Grid, GridItem, Heading } from '@chakra-ui/react'
import React from 'react'
import StoreCard from 'components/StoreCard'

export default function Store() {
	const cardData = [
		{
			isNew: true,
			imageURL: 'https://source.unsplash.com/random/240x240&1',
			name: 'Lorem Ipsum Dolor Sit Amet',
			price: 9.99,
		},
		{
			isNew: false,
			imageURL: 'https://source.unsplash.com/random/240x240&2',
			name: 'Consectetur Adipiscing Elit',
			price: 7.99,
		},
		{
			isNew: false,
			imageURL: 'https://source.unsplash.com/random/240x240&3',
			name: 'Sed Do Eiusmod Tempor',
			price: 12.99,
		},
		{
			isNew: true,
			imageURL: 'https://source.unsplash.com/random/240x240&4',
			name: 'Incididunt Ut Labore Et Dolore',
			price: 6.99,
		},
	]
	return (
		<>
			<Heading mb="2">Store</Heading>

			<Grid templateColumns="repeat(4, 1fr)" gap={4}>
				{cardData.map((card) => (
					<GridItem key={card.name} w="100%" h="320px">
						<StoreCard
							isNew={card.isNew}
							imageURL={card.imageURL}
							name={card.name}
							price={card.price}
						/>
					</GridItem>
				))}
			</Grid>
		</>
	)
}
