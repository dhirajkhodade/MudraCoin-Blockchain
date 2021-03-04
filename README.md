# MudraCoin-Blockchain
This is simple blockchain implementation in C# and UI in aspnet core razor pages.

Check out my medium post for detailed explaination about this code - [Post](https://dhirajkhodade.medium.com/blockchain-explained-using-c-implementation-5482dc980c47)

# Features 
- Create Blockchain
- Mining Rewards & Transactions
- Validate and Sign Transactions (using Elliptic curves secp256k1)
- Proof-of-Work for mining Blocks
- Front-End in aspnet core for easy understanding of Blockchain functionality

# Docker support

I have added docker image to docker hub.
Now you can run it as docker container directly on your machine with following command and then access it from your browser - http://localhost:8080

```
docker run -p 8080:80 dhirajkhodade/mudracoin-blockchain-app
```

Or build your own image locally
For that goto Dashboard directory and run below command to build docker image

```
 docker image build --tag [your docker id]/mudracoin-blockchain-app -f Dockerfile ..
```

and then run the image and access it from browser - http://localhost:8080

```
docker run -p 8080:80 dhirajkhodade/mudracoin-blockchain-app
```


![UI Dashboard](https://github.com/dhirajkhodade/MudraCoin-Blockchain/blob/main/ScreenShot.png?raw=true)

