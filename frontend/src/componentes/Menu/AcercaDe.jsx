import React from "react";
import "./AcercaDe.css";



const AcercaDe = () => {
    return (
        <div className="container">
            <section className="content">
                <h1 className="title">¿Quienes somos?</h1>
                <p className="description"> Somos una empresa especializada en ofrecer soluciones inteligentes para la <b>gestión de recursos humanos</b>. Nuestra misión es optimizar los procesos internos de las organizaciones mediante herramientas tecnológicas que simplifiquen y automatizen la administración de vacaciones, permisos y el registro de asistencias de los empleados.</p>
                <p className="description">Somos una plataforma lider, destacándonos por nuestra innovación, simplicidad y capacidad de adaptarnos a las necesidades de empresas de todos los tamaños.</p>
                <h2 className="title">Nuestro Compromiso</h2>
                <p className="description">En <b>WORKSENSE</b>, estamos comprometidos con facilitar las relaciones entre empleados y empleadores, creando un entorno laboral más organizado, eficiente y productivo. Apostamos por la digitalización como el camino hacia un futuro empresarial más competitivo y humano.</p>
            </section>
            <footer className="footer">
                <div className="footer-section">
                    <h4>Acerca de Worksense</h4>
                    <ul>
                        <li><a href="#">Nuestra Plataforma</a></li>
                        <li><a href="#">Politica de Privacidad</a></li>
                        <li><a href="#">Soporte técnico</a></li>
                    </ul>
                </div>
                <div className="footer-section">
                    <h4>Recursos</h4>
                    <ul>
                        <li><a href="#">Preguntas frecuentes</a></li>
                        <li><a href="#">uías de usuario</a></li>
                        <li><a href="#">Blog</a></li>
                    </ul>
                    
                </div>
                <div className="footer-section">
                    <h4>Contacto</h4>
                    <ul>
                        <li><a href="#">Email de contacto</a></li>
                        <li><a href="#">Redes Sociales</a></li>
                    </ul>
                    
                </div>
                
            </footer>
        </div>
    )
}

export default AcercaDe;