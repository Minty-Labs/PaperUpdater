<h1>PaperUpdater</h1>
Simple <a href="https://papermc.io/downloads" target="_blank">papermc.io</a> Minecraft Server file updater. Gets the latest version, downloads it and places the file in the root of where the program EXE is. Detailed process below.

<h2>How it works</h2>
Logic below is held and processed in <a href="PaperConsoleUpdater/PaperApiJson.cs">PaperApiJson.cs</a>
<ul>
	<li>Loading Paper API JSON Data
		<ul>
			<li>Gets JSON data from API URL:
				<ul>
					<li>API URL: `https://api.papermc.io/v2/projects/paper/versions/1.19.1/builds`</li>
					<li>Outputs: JSON structured data with every build of paper 1.19.1</li>
				</ul>
			</li>
			<li>It gets the last entry in the list of `builds` (last is latest)</li>
			<li>It then verifies that the `channel` is `default`</li>
			<li>Outputs a completed URL `https://api.papermc.io/v2/projects/paper/versions/1.19.1/builds/{buildNumber}/downloads/{buildName}`</li>
		</ul>
	</li>
	<li>Updating the file
		<ul>
			<li>If Data is not incorrect</li>
			<li>It gets the file data as bytes from the completed URL constructed above</li>
			<li>It will delete a file `paper.jar` if it exists</li>
			<li>Then write a new file with the bytes gathered from the completed URL and saves it as `paper.jar`</li>
		</ul>
	</li>
</ul>

<h2>To Do</h2>
<ul>
	<li>Manual input of minecraft version, instead of a static version</li>
	<li>Creation of batch file method</li>
	<li>more ...</li>
</ul>