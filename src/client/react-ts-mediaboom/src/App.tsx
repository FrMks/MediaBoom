import {
  Music,
  Eye,
  Heart,
  LockKeyhole,
  UserRound,
  Zap,
} from 'lucide-react'

import { Button } from './components/ui/button'
import './App.css'

const benefits = [
  {
    icon: Zap,
    title: 'Мгновенный доступ',
    description: 'Слушай любимую музыку без задержек',
  },
  {
    icon: Music,
    title: 'Большая библиотека',
    description: 'Фильмы, сериалы, шоу и многое другое',
  },
  {
    icon: Heart,
    title: 'Персональный опыт',
    description: 'Рекомендации и закладки для тебя',
  },
]

function App() {
  return (
    <main className="login-page">
      <section className="login-shell" aria-label="Вход в MediaBoom">
        <aside className="login-showcase">
          <a className="brand" href="/" aria-label="MediaBoom — на главную">
            <span className="brand-mark">MB</span>
            <span className="brand-name">
              Media<span>Boom</span>
            </span>
          </a>

          <div className="showcase-copy">
            <p className="showcase-kicker">Твой контент.</p>
            <h1>
              Твой <span>взрыв эмоций.</span>
            </h1>
            <p className="showcase-description">
              Любимые фильмы, сериалы и шоу в высоком качестве без ограничений.
            </p>
          </div>

          <ul className="benefit-list">
            {benefits.map(({ icon: Icon, title, description }) => (
              <li key={title}>
                <span className="benefit-icon">
                  <Icon aria-hidden="true" />
                </span>
                <span>
                  <strong>{title}</strong>
                  <small>{description}</small>
                </span>
              </li>
            ))}
          </ul>

          <p className="copyright">© 2026 MediaBoom. Все права защищены.</p>
        </aside>

        <div className="login-panel">
          <div className="auth-tabs" role="tablist" aria-label="Авторизация">
            <button className="auth-tab is-active" type="button" role="tab" aria-selected="true">
              Вход
            </button>
            <button className="auth-tab" type="button" role="tab" aria-selected="false">
              Регистрация
            </button>
          </div>

          <form className="login-form" onSubmit={(event) => event.preventDefault()}>
            <label className="field">
              <span className="sr-only">Email или имя пользователя</span>
              <UserRound aria-hidden="true" />
              <input
                name="login"
                type="text"
                placeholder="Email или имя пользователя"
                autoComplete="username"
              />
            </label>

            <label className="field">
              <span className="sr-only">Пароль</span>
              <LockKeyhole aria-hidden="true" />
              <input
                name="password"
                type="password"
                placeholder="Пароль"
                autoComplete="current-password"
              />
              <button className="password-toggle" type="button" aria-label="Показать пароль">
                <Eye aria-hidden="true" />
              </button>
            </label>

            <div className="form-options">
              <label className="remember-me">
                <input type="checkbox" name="remember" defaultChecked />
                <span>Запомнить меня</span>
              </label>
              <a href="/forgot-password">Забыли пароль?</a>
            </div>

            <Button
              className="login-submit"
              variant="mediaboomLoginBtn"
              size="loginBtnSize"
              type="submit"
            >
              Войти
            </Button>

            <p className="signup-link">
              Ещё нет аккаунта? <a href="/register">Зарегистрироваться</a>
            </p>
          </form>
        </div>
      </section>
    </main>
  )
}

export default App
