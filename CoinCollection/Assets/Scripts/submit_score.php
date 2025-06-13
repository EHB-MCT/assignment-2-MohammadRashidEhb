<?php
// Database credentials
$servername = "localhost"; // This is the typical value for shared hosting
$username = "2324DEV3044"; // Your MySQL username (use the database name here as you've mentioned)
$password = "ZXN8Hds"; // Your MySQL password
$dbname = "2324DEV3044"; // Your database name

// Create a connection to the MySQL database
$conn = new mysqli($servername, $username, $password, $dbname);

// Check if the connection is successful
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Get the data sent via POST from Unity
$player_name = $_POST['player_name']; // The player's name (from Unity)
$score = $_POST['score']; // The score (from Unity)

// Prepare and execute the SQL query to insert the score into the database
$stmt = $conn->prepare("INSERT INTO CoinGameScores (player_name, score) VALUES (?, ?)");
$stmt->bind_param("si", $player_name, $score); // "si" means: string, integer

// Execute the query
if ($stmt->execute()) {
    echo "Score added successfully!";
} else {
    echo "Error: " . $stmt->error;
}

// Close the database connection
$stmt->close();
$conn->close();
?>
