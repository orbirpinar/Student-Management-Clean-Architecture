import fetch from "node-fetch";
import jsdom from 'jsdom';
import csvWriter from 'csv-writer';


const { JSDOM } = jsdom;


const writer = csvWriter.createObjectCsvWriter({
  path: '../Infrastructure/Persistence/teachers',
  header: [
    { id: 'fullName', title: "FullName" },
    { id: 'email', title: "Email" },
    { id: 'subject', title: "Subject" },
    { id: 'subject_turkish', title: "SubjectTurkish" }
  ]
});


(async () => {

  const url = 'https://filibeliahmethilmiiho.meb.k12.tr/34/26/766331/teskilat_semasi.html';
  const response = await fetch(url);
  const text = await response.text();
  const dom = await new JSDOM(text);

  const subjectConverter = {
    "Din Kültürü ve Ahlak Bilgisi": "Education of Religion and Ethics",
    "Fen ve Teknoloji": "Science",
    "Psikolojik Danışman": "Psychological Counselor",
    "Görsel Sanatlar": "Art",
    "Matematik": "Math",
    "İngilizce": "English",
    "Türkçe": "Turkish",
    "Beden Eğitimi": "Gym",
    "Sosyal Bilgiler": "Social Studies",
    "Teknoloji ve Tasarım": "Technology and Design",
    "Bilişim Teknolojileri ve Yazılım": "Information Technologies"
  }


  const nodes = dom.window.document.querySelectorAll(".row .sitemap ul li a")


  const teacherList = [...nodes].map(node => {
    const fullName = node.childNodes[0].nodeValue;

    return {
      'fullName': fullName,
      'subject': subjectConverter[node.querySelector("span").textContent],
      'subject_turkish': node.querySelector("span").textContent,
      'email': `${fullName.replaceAll(" ", ".").toLowerCase()}@example.com`
    }
  })

  writer
    .writeRecords(teacherList)
    .then(() => console.log('The CSV file was written successfully'));



})()

