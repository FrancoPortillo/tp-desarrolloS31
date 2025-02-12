import Card from './Card'
import image1 from '../../assets/image1.jpg'
import image2 from '../../assets/image2.jpg'
import image3 from '../../assets/image3.jpg'

const cards = [
    {
    id: 1,
    title: 'Noticias y actualizaciones importantes',
    image: image1,
    url: 'https://fazweb.com',
    text: 'Aquí encontrarás las últimas noticias y anuncios relacionados con la empresa, eventos próximos, y cambios relevantes en los procedimientos internos. Mantente al tanto de todo lo que sucede.'
    },
    {
      id: 2,
      title: 'Recursos y Herramientas',
      image: image2,
      url: 'https://blog.faztweb.com',
      text:"Accede fácilmente a una variedad de recursos y herramientas que la empresa pone a tu disposición para facilitar tu trabajo. Aquí encontrarás manuales, guías,y enlaces útiles que te permitirán mejorar tu productividad y gestionar tus tareas diarias."

    },
    {
      id: 3,
      title:'Acerca de WorkSense',
      image: image3,
      url: 'https://youtube.com',
      text: "Conoce más sobre nuestra empresa, su historia, misión, visión y los valores que nos guían en nuestro día a día. En esta sección podrás descubrir las metas que nos motivan. Aprende más sobre lo que hace especial a nuestra organización"
    }
]

function Cards() {
  console.log(cards)
  return (
    <div className='container'>
      
      <div className='d-flex justify-content-center align-items-center h-100'>
        <div className='row'>
          {
            cards.map((card) => (
              <div className='col-md-4' key={card.id}>
                <Card title={card.title} imageSource={card.image} url={card.url} text={card.text}/>
              </div>
            ))
          }
        </div>
      </div>
    </div>
  )
}

export default Cards