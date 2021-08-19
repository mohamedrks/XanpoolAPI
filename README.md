Xanpool back end API application.

1. setup local Mongodb.

	a. use BookCatalogDB
	
	b. db.createCollection('Books')
	
	c. db.Books.insertMany([{'Title': 'Native Cloud Computing', 'Year':'2020', 'Description':' Book briefs about modern cloud computing.'}, 
				{'Title' : 'Kubernetes with Docker', 'Year': '2021', 'Description' : 'The book is about docker usage on kubernetes cluster.'}
				{'Title': 'Nginx with Microservices', 'Year':'2019', 'Description':' Book briefs about Nginx usage with microservices.'} ,
				{'Title' : 'Microservices with K8S &  Docker', 'Year': '2021', 'Description' : 'Book having many hands on lab excersises using K8S and Docker on microservies.'}		
			      ])

2. make sure the "BookCatalogDbSettings" is configured on appsettings.json to access MongoDb


3. Run the back end application and make sure the API url is correctly configured on fron end Api url.
