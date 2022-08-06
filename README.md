<h1>PaperUpdater</h1>
Simple <a href="https://papermc.io/downloads" target="_blank">papermc.io</a> Minecraft Server file updater. Gets the latest version, downloads it and places the file in the root of where the program EXE is. Detailed process below.

<h2>How it works</h2>
Logic below is held and processed in <a href="PaperConsoleUpdater/PaperApiJson.cs">PaperApiJson.cs</a>
<ul>
	<li>Loading Paper API JSON Data
		<ul>
			<li>Gets JSON data from API URL:
				<ul>
					<li>API URL: <code>https://api.papermc.io/v2/projects/paper/versions/1.19.2/builds</code></li>
					<li>Outputs: JSON structured data with every build of paper 1.19.2</li>
				</ul>
			</li>
			<li>It gets the last entry in the list of <code>builds</code> (last is latest)</li>
			<li>It then verifies that the <code>channel</code> is <code>default</code></li>
			<li>Outputs a completed URL <code>https://api.papermc.io/v2/projects/paper/versions/1.19.2/builds/{buildNumber}/downloads/{buildName}</code></li>
		</ul>
	</li>
	<li>Updating the file
		<ul>
			<li>If Data is not incorrect</li>
			<li>It gets the file data as bytes from the completed URL constructed above</li>
			<li>It will delete a file <code>paper.jar</code> if it exists</li>
			<li>Then write a new file with the bytes gathered from the completed URL and saves it as <code>paper.jar</code></li>
		</ul>
	</li>
	<li>Running the server
		<ul>
			<li>You have the ability to create a basic preset batch file</li>
			<li>You can also have this application run that batch file directly</li>
		</ul>
	</li>
</ul>

<h2>To Do</h2>
<ul>
	<li>Get Total latest PaperMC version</li>
	<li>Maybe have the application directly run the server from entirely within</li>
	<li>more ...</li>
</ul>

<h2>Credits and Ownerships</h2>
<ul>
	<li>Program Icon (Logo) is directly from papermc's website header logo. All rights go to papermc for the logo. <i>If someone from the papermc team has an issue with me using your logo, please do not hesitate to email <a href="mailto:admin@mintlily.lgbt">Lily C.</a></i></li>
</ul>

<h2>Disclamer</h2>
I, Lily, am in no way affiliacted with PaperMC nor Microsoft/Mojang Studios. Any and all images' rights go their original owners.<br>

<h2>Application Info</h2>
<ul>
	<li>Type: <code>Console Application (dotNET 6)</code></li>
	<li>Version: <code>v1.3.0.0</code></li>
	<li>Nuget Packages: <code>Newtonsoft.Json - 13.0.2-beta1</code></li>
	<li>Checksum SHA-256: <code>72b4da9c9bae00785a76f8c0a336c7248b79c2e27963e7219b2891ceb882d4ee</code></li>
</ul>