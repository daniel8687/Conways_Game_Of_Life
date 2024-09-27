# Conways_Game_Of_Life
Conway's Game of Life

# REST API End Points
![alt text](https://github.com/[username]/[reponame]/blob/[branch]/image.jpg?raw=true)

# Run Docker Image
go to ..\Conways_Game_Of_Life\Conways_Game_Of_Life
run docker build -t conways_game_of_life -f Dockerfile .
run docker run --rm -d -e ASPNETCORE_ENVIRONMENT=Development -p 5001:80 --name conways_game_of_life_container conways_game_of_life
go to http://localhost:5001/swagger/index.html in your browser
