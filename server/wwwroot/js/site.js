// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    console.log('APP PAGE LOADED')

    var loginButton = document.getElementById('btn-login');

    checkWindowUrlForAccessToken();

    loginButton.addEventListener('click', function () {
        login();
    });

    function login() {
        console.log('login function')
        var CLIENT_ID = '5d5395837de4440c9f62c6e43acd4094';
        var REDIRECT_URI = 'http://localhost:5000';
        function getLoginURL(scopes) {
            return 'https://accounts.spotify.com/authorize?client_id=' + CLIENT_ID +
                '&redirect_uri=' + encodeURIComponent(REDIRECT_URI) +
                '&scope=' + encodeURIComponent(scopes.join(' ')) +
                '&response_type=token';

        }

        var url = getLoginURL([
            'user-read-email',
            'user-top-read'
        ]);

        var width = 450,
            height = 730,
            left = (screen.width / 2) - (width / 2),
            top = (screen.height / 2) - (height / 2);

        var w = window.location.href = url;

        console.log('w', w)
    }

    function getUserData(accessToken) {
        console.log('get user data', accessToken)
        return $.ajax({
            url: 'https://api.spotify.com/v1/me/top/artists?limit=5',
            headers: {
                'Authorization': 'Bearer ' + accessToken
            }
        });
    }

    function checkWindowUrlForAccessToken() {
        // Attempt to extract access token from URL if "access_token=" text is found
        var accessToken = window.location.href.split(/[#&]+/).filter(string => string.startsWith("access_token="))[0];

        if (accessToken) {
            // remove the "access_token=" text from the actual token data.
            accessToken = accessToken.replace("access_token=", "");

            getUserData(accessToken)
                // After user data is received
                .then(function (response) {
                    console.log(response)
                    console.log(JSON.stringify(response))
                    loginButton.style.display = 'none';
                    renderTopArtists(response);
                })
                .catch((err) => console.log(err));
        }
    }

    function renderTopArtists(topArtists) {

        /*
        Alternatively, you can build html as a string to append to page

        `
        <div>
            <h2 class="text-center">${artist.name}</h2>
        </div>
        `
        */
        
        const resultsDiv = document.getElementById('result');
        const topFiveArtists = topArtists.items.slice(0, 5);

        const topArtistsContainer = document.createElement("div")

        for (let i = 0; i < topFiveArtists.length; i++) {
            const artistDiv = document.createElement("div");
            const artist = topFiveArtists[i];
            console.log(artist.name)
            
            const h2Name = document.createElement("h2");
            h2Name.innerText = artist.name;
            artistDiv.appendChild(h2Name);

            const firstImage = artist?.images?.[0];

            if (firstImage) {
                const imgElem = document.createElement("img");
                imgElem.src = firstImage.url;
                imgElem.classList.add("border","rounded", "border-primary","w-25", "h-25")
                artistDiv.appendChild(imgElem);
            }

            topArtistsContainer.append(artistDiv);
        }

        resultsDiv.appendChild(topArtistsContainer);
    }
})();


// <div class="container">
// <h1>Displaying User Data</h1>
// <p>Log in with your Spotify account and this demo will display information about you fetched using the Spotify Web API</p>
// <button class="btn btn-primary" id="btn-login">Login</button>
// <div id="result">random things</div>
// </div>