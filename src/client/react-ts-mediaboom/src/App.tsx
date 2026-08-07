import {
  Clapperboard,
  Eye,
  Heart,
  LockKeyhole,
  UserRound,
  Zap,
} from 'lucide-react'

import { Button } from '@/components/ui/button'
import { Checkbox } from '@/components/ui/checkbox'
import {
  InputGroup,
  InputGroupAddon,
  InputGroupButton,
  InputGroupInput,
} from '@/components/ui/input-group'
import { MediaBoomLogo } from '@/components/ui/media-boom-logo'
import { Separator } from '@/components/ui/separator'
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs'
import './App.css'

const benefits = [
  {
    icon: Zap,
    title: 'Мгновенный доступ',
    description: 'Смотри любимые медиа без задержек',
  },
  {
    icon: Clapperboard,
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
          <MediaBoomLogo href="/" aria-label="MediaBoom — на главную" />

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
          <Tabs value="login" className="w-full">
            <TabsList variant="line" className="mb-12 grid w-full grid-cols-2">
              <TabsTrigger value="login">Вход</TabsTrigger>
              <TabsTrigger value="register">Регистрация</TabsTrigger>
            </TabsList>

            <TabsContent value="login">
              <form
                className="grid gap-4.5"
                onSubmit={(event) => event.preventDefault()}
              >
                <InputGroup>
                  <InputGroupInput
                    aria-label="Email или имя пользователя"
                    name="login"
                    type="text"
                    placeholder="Email или имя пользователя"
                    autoComplete="username"
                  />
                  <InputGroupAddon align="inline-start">
                    <UserRound aria-hidden="true" />
                  </InputGroupAddon>
                </InputGroup>

                <InputGroup>
                  <InputGroupInput
                    aria-label="Пароль"
                    name="password"
                    type="password"
                    placeholder="Пароль"
                    autoComplete="current-password"
                  />
                  <InputGroupAddon align="inline-start">
                    <LockKeyhole aria-hidden="true" />
                  </InputGroupAddon>
                  <InputGroupAddon align="inline-end">
                    <InputGroupButton aria-label="Показать пароль">
                      <Eye aria-hidden="true" />
                    </InputGroupButton>
                  </InputGroupAddon>
                </InputGroup>

                <div className="my-1 flex items-center justify-between gap-4 text-xs">
                  <label className="inline-flex cursor-pointer items-center gap-2.5 text-white/65">
                    <Checkbox name="remember" defaultChecked />
                    <span>Запомнить меня</span>
                  </label>
                  <a
                    className="font-medium text-amber-400 no-underline hover:underline"
                    href="/forgot-password"
                  >
                    Забыли пароль?
                  </a>
                </div>

                <Button
                  className="h-14 w-full rounded-lg text-base"
                  variant="mediaboomLoginBtn"
                  size="loginBtnSize"
                  type="submit"
                >
                  Войти
                </Button>

                <p className="mt-4 text-center text-xs text-white/50">
                  Ещё нет аккаунта?{' '}
                  <a
                    className="font-medium text-amber-400 no-underline hover:underline"
                    href="/register"
                  >
                    Зарегистрироваться
                  </a>
                </p>
              </form>
            </TabsContent>
          </Tabs>
        </div>
      </section>
    </main>
  )
}

export default App
