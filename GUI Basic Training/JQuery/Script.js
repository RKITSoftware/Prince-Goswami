let topicList = [
  "age",
  "alone",
  "amazing",
  "anger",
  "anniversary",
  "architecture",
  "art",
  "attitude",
  "beauty",
  "best",
  "birthday",
  "business",
  "car",
  "change",
  "communication",
  "computers",
  "cool",
  "courage",
  "dad",
  "dating",
  "death",
  "design",
  "diet",
  "dreams",
  "education",
  "environmental",
  "equality",
  "experience",
  "failure",
  "faith",
  "family",
  "famous",
  "fear",
  "finance",
  "fitness",
  "food",
  "forgiveness",
  "freedom",
  "friendship",
  "funny",
  "future",
  "gardening",
  "god",
  "good",
  "government",
  "graduation",
  "great",
  "happiness",
  "health",
  "history",
  "home",
  "hope",
  "humor",
  "imagination",
  "inspirational",
  "intelligence",
  "jealousy",
  "knowledge",
  "leadership",
  "learning",
  "legal",
  "life",
  "love",
  "marriage",
  "medical",
  "men",
  "mom",
  "money",
  "morning",
  "motivational",
  "movies",
  "movingon",
  "music",
  "nature",
  "parenting",
  "patience",
  "patriotism",
  "peace",
  "pet",
  "poetry",
  "politics",
  "positive",
  "power",
  "relationship",
  "religion",
  "respect",
  "romantic",
  "sad",
  "science",
  "smile",
  "society",
  "sports",
  "strength",
  "success",
  "sympathy",
  "teacher",
  "technology",
  "teen",
  "thankful",
  "time",
  "travel",
  "trust",
  "truth",
  "war",
  "wedding",
  "wisdom",
  "women",
  "work",
  "christmas",
  "easter",
  "fathersday",
  "memorialday",
  "mothersday",
  "newyears",
  "saintpatricksday",
  "thanksgiving",
  "valentinesday"
]

// Get the p tag and cite tag elements.
const pTag = $('#quote');
const citeTag = $('#label');
const quoteBlock = $('#quote-block');
$('#alert-msg').hide();

pTag.hide();

$(document).ready(() => {


  // const url = 'https://quotes85.p.rapidapi.com/getrandomquote';
  // const options = {
  // 	method: 'GET',
  // 	headers: {
  // 		'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
  // 		'X-RapidAPI-Host': 'quotes85.p.rapidapi.com'
  // 	}
  // };

  const url = 'https://652b83fb4791d884f1fdd9d5.mockapi.io/QuoteGen/Quotes'
  const options = {};

  // Set the text of the cite tag.
  fetchData(url, options)
    .then((result) => {
      result.map((data) => {
        pTag.text(data.Quote);
        citeTag.text(data.author);
        pTag.show(2000);
        console.log(data.author);
        //citeTag.text(data.author);
        // You can access the 'result' value here
        //console.log("Accessing result outside of the function:", result);
      });
    })
    .catch((error) => {
      console.error("Error occurred:", error);
    });
});


async function fetchData(url, options) {
  try {
    const response = await fetch(url, options);
    const result = JSON.parse(await response.text());
    return result;
  } catch (error) {
    console.error(error);
    throw error;
  }
}


$('#GRQ').click(async () => {
  const url = 'https://quotes15.p.rapidapi.com/quotes/random/';
  const options = {
    method: 'GET',
    headers: {
      'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
      'X-RapidAPI-Host': 'quotes15.p.rapidapi.com'
    }
  };
  // const url = 'https://famous-quotes4.p.rapidapi.com/random?category=all&count=2';
  // const options = {
  //   method: 'GET',
  //   headers: {
  //     'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
  //     'X-RapidAPI-Host': 'famous-quotes4.p.rapidapi.com'
  //   }
  // };
  fetchData(url, options)
    .then((data) => {
      console.log(data);
      //console.log(data.originator);
      pTag.hide(() => {
        console.log(data.originator.name);
        pTag.text(data.content);
        citeTag.text(data.originator.name);
      });
      pTag.show(1000);

    })
    //citeTag.text(data.author);
    .catch((error) => {
      console.error("Error occurred:", error);
    });
});

$('#QOTD').click(() => {
  const url = 'https://quotes-inspirational-quotes-motivational-quotes.p.rapidapi.com/quote?token=ipworld.info';
  const options = {
    method: 'GET',
    headers: {
      'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
      'X-RapidAPI-Host': 'quotes-inspirational-quotes-motivational-quotes.p.rapidapi.com'
    }
  };

  fetchData(url, options)
    .then((data) => {
      pTag.hide(() => {
        pTag.text(data.text);
        citeTag.text(data.author);
        console.log(data.author);
        pTag.show(1000);
        // quoteBlock.show(2000);
      });
      //citeTag.text(data.author);
      // You can access the 'result' value here
      //console.log("Accessing result outside of the function:", result);
    })
    .catch((error) => {
      console.error("Error occurred:", error);
    });

});

$('#SBK').click(() => {
  let topic = $('#keyword').val();
  console.log(topic);
  topic = topicList.includes(topic.toLowerCase()) ? topic.toLowerCase() : 'all';
  console.log(topic);
  const url = `https://famous-quotes4.p.rapidapi.com/random?category=${topic ? topic : 'all'}&count=1`;
  const options = {
    method: 'GET',
    headers: {
      'X-RapidAPI-Key': 'fb9a25b181msh5c88c6d0aa36a4bp128766jsn26c140c6f064',
      'X-RapidAPI-Host': 'famous-quotes4.p.rapidapi.com'
    }
  };

  fetchData(url, options)
    .then((result) => {
      console.log(result);
      result.map((data) => {
        console.log(data);
        console.log(data.originator);
        pTag.hide(() => {
          //console.log(data.author);
          pTag.text(data.name);
          citeTag.text(data.author);
        });
        pTag.show(1000);
      });
    })
    //citeTag.text(data.author);
    .catch((error) => {
      console.error("Error occurred:", error);
    });
});


$('#tw-share').click(() => {
  console.log('Share ');
  console.log(pTag.text() + ' ' + citeTag.text());
})

$('#fb-share').click(() => {
  console.log(pTag.text() + ' ' + citeTag.text());
  window.open(`https://www.facebook.com/sharer/sharer.php?u=${encodeURIComponent(pTag.text() + ' ' + citeTag.text())}`);
})

$('#in-share').click(() => {
  console.log(pTag.textContent + ' ' + citeTag.textContent);
  window.open(`https://www.instagram.com/share/?text=${encodeURIComponent(pTag.text() + ' ' + citeTag.text())}`);
})

$('#wp-share').click(() => {
  console.log(pTag.textContent + ' ' + citeTag.textContent);
})

$('#clipboard').click(() => {

  // Get the text to copy.
  const textToCopy = pTag.text()  + ' ' + citeTag.text();
  $('#clipboard').hide();
  $('#alert-msg').text('copied to clipboard!!');
  // Copy the text to the clipboard.
  navigator.clipboard.writeText(textToCopy);
  $('#alert-msg').fadeIn();
  $('#alert-msg').fadeOut();
  $('#clipboard').show();


});
