import "./Boton.css";

export const Boton = ({ texto, onClick }) => {
  return (
    <button className="boton" onClick={onClick}>
      {texto}
    </button>
  );
};

export default Boton;