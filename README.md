<h1>PaperUpdater</h1>
Simple <a href="https://papermc.io/downloads" target="_blank">papermc.io</a> or <a href="https://purpurmc.org/" target="_blank">purpurmc.org</a> Minecraft Server file updater. Gets the latest version, downloads it and places the file in the root of where this program's executable file is. Detailed process below.

<h2>How it works (PaperMC)</h2>
Logic below is held and processed in <a href="PaperConsoleUpdater/PaperData/PaperProjectApi.cs">PaperProjectApi.cs</a>
<ul>
	<li>Getting the latest paper project version
		<ul>
			<li>Gets JSON data from API URL:
				<ul>
					<li>API URL: <code>https://api.papermc.io/v2/projects/paper</code></li>
					<li>Outputs: JSON structured data with every version group and version offered to use</li>
				</ul>
			</li>
			<li>It finds the list of strings in <code>versions</code> (last is latest)</li>
			<li>It verifies that the <code>project_id</code> is <code>paper</code></li>
			<li>Stores it in a string that should be <code>1.19.3</code></li>
		</ul>
	</li>
</ul>

Logic below is held and processed in <a href="PaperConsoleUpdater/PaperData/PaperBuildApi.cs">PaperBuildApi.cs</a>
<ul>
	<li>Loading Paper API JSON Data
		<ul>
			<li>Gets JSON data from API URL:
				<ul>
					<li>API URL: <code>https://api.papermc.io/v2/projects/paper/versions/1.19.3/builds</code></li>
					<li>Outputs: JSON structured data with every build of paper 1.19.3</li>
				</ul>
			</li>
			<li>It gets the last entry in the list of <code>builds</code> (last is latest)</li>
			<li>It then verifies that the <code>channel</code> is <code>default</code></li>
			<li>Outputs a completed URL <code>https://api.papermc.io/v2/projects/paper/versions/1.19.3/builds/{buildNumber}/downloads/{buildName}</code></li>
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
</ul>

<h2>How it works (PurpurMC)</h2>
Their API easily lets me only get the latest version of their server. It doesn't have to do any of the logic I did for PaperMC. I just get the latest version and download it. Simple as that.<br>
Logic below is held and processed in <a href="PaperConsoleUpdater/PaperData/PaperBuildApi.cs#L53">PaperProjectApi.cs</a> Line 53, which gets applied on <a href="PaperConsoleUpdater/PaperData/PaperBuildApi.cs#L79">line 79</a>

<h2>How it works (Overall)</h2>
Logic below is held and processed in <a href="PaperConsoleUpdater/Functions/BatchFuncs.cs">BatchFuncs.cs</a>
<ul>
	<li>Creating a batch file
		<ul>
			<li>
				Creates a simple batch file to easily run your server
			</li>
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
	<li>hmm, not sure what else to do</li>
</ul>

<h1>Linux</h1>
Recommended actions needed:
<ul>
	<li>Install .NET 6.0+ <a href="https://docs.microsoft.com/en-us/dotnet/core/install/linux" target="_blank">Using Microsoft's Guide</a>.</li>
	<li>Install Screen <a href="https://linuxhint.com/screen-linux/" target="_blank">Using this guide</a>.</li>
</ul>
Running it:
<ul>
	<li>Install screen (above)</li>
	<li>create and change into a new screen</li>
	<li><code>cd</code> into your desired folder (Preferably where your server is installed)</li>
	<li><code>wget</code> the latest version ( <code>wget https://github.com/Minty-Labs/PaperUpdater/releases/latest/download/PaperConsoleUpdater</code> )</li>
	<li>make sure to <code>sudo chmod +x PaperConsoleUpdater</code></li>
	<li>Run the program (<code>./PaperConsoleUpdater</code>)</li>
</ul>

<h2>Credits and Ownerships</h2>
<ul>
	<li>Program Icon (Logo) is directly from papermc's website header logo. All rights go to papermc for the logo. <i>If someone from the papermc team has an issue with me using your logo, please do not hesitate to email <a href="mailto:admin@mintlily.lgbt">Lily C.</a></i></li>
</ul>

<h2>Disclaimer</h2>
I, Lily, am in no way affiliated with PaperMC nor Microsoft/Mojang Studios. Any and all images' rights go their original owners.<br>

<h2>Application Info</h2>
<ul>
	<li>Type: <code>Console Application (dotNET 6)</code></li>
    <li>IDE: <code>JetBrains Rider - 2022.3.1</code></li>
	<li>Version: <code>v1.5.0.0</code></li>
	<li>Nuget Packages:
        <ul>
            <li><code>Newtonsoft.Json - 13.0.2</code></li>
            <li><code>Pastel - 4.1.0</code></li>
        </ul>
    </li>
	<li>Checksum SHA-256
		<ul>
			<li>Windows: <code>1f77cfae88a39c75b1fa0e9e2c3b172fba48ac2521e4951b267f120a66b6e38d</code></li>
			<li>Linux:   <code>6da4bf58067a2a5b0627bc7479793447d7216d6fe49c0074f9da888c56818919</code></li>
		</ul>
	</li>
</ul>