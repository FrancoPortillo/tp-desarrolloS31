import PropTypes from 'prop-types'
import './cards.css'

function Card({title,imageSource,text ,url}) {
  return (
    <div className="card text-center bg-dark animate__fadeInUp">
     <img src={imageSource} alt="" className="card-img-top"/>
      <div className="card-body text-light">
        <h4 className="card-title">{title}</h4>
        <p className="card-text text-secondary">
          {
            text ? text : 'Lorem ipsum dolor sit, amet consectetur adipisicing elit. Odit, accusantium doloremque soluta ea sequi quasi quod harum minus ad, deserunt facilis maxime tempora atque dolorem nisi, ducimus beatae eligendi dignissimos eos! Magni minus, sed quas nisi laboriosam asperiores molestiae consequatur?'
          }
        </p>
          <a href={"https:localhost:5173/perfil/35"} className='btn btn-outline-secondary rounded-0' target='_blank'>
            CLICK AQUÍ
          </a>
      </div>
    </div>
  )
}

Card.propTypes = {
  title: PropTypes.string.isRequired,
  url: PropTypes.string,
  imageSource: PropTypes.string,
  text: PropTypes.string
}

export default Card